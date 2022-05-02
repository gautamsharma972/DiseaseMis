import { Component, OnInit } from '@angular/core';
import { ApiService } from "../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";
import { moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-samples',
  templateUrl: './samples.component.html',
  styleUrls: ['./samples.component.css']
})
export class SamplesComponent implements OnInit {

  public content: any = {
    title: '',
    name: "Samples",
    data: [],
    fields: [],
    errorMessage: "",
    apiLink: '/samples',
    caller: '',
    Samples: {
      testTypes: [
      ]
    }
  }

  searchTerm: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService) {
  }

  ngOnInit(): void {
    this.get();
  }

  private async get() {
    await this.apiService.getData('/samples', null)
      .subscribe((res: any) => {
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.length <= 0) {
            this.content.errorMessage = "No Samples found. Click 'Add New' to add.";
          }
          this.content.data = res;
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }


  drop(event: any, sampleData: any) {
    moveItemInArray(this.content.data, event.previousIndex, event.currentIndex);

    sampleData.forEach(function (_data: any, index: number) {
      _data.listingOrder = index
    });

    this.apiService.postData('/samples/updateListingOrder', sampleData).subscribe((res: any) => {
      //
    })
  }

  create() {
    this.content.title = `Add ${this.content.name}`;
    this.content.caller = 'POST';
    this.content.Samples = {
      testTypes: []
    };
  }

  edit(item: any) {
    this.content.title = `Edit ${this.content.name}`;
    this.content.caller = 'PUT';
    this.content.Samples = item;
  }

  delete(id: any) {
    this.apiService.deleteData(`${this.content.apiLink}/${id}`, id).subscribe((res: any) => {
      if (res) {
        this.toast.success("Item Deleted", {
          position: 'top-right',
          autoClose: true
        });
      }

    }, error => {
      this.toast.error("Error deleting, try again after sometime.", {
        position: 'top-right',
        autoClose: true
      });
    });
  }
}
