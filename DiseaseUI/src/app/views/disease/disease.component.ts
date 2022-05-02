import { Component, OnInit } from '@angular/core';
import { ApiService } from "../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";
import { moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-disease',
  templateUrl: './disease.component.html',
  styleUrls: ['./disease.component.css']
})
export class DiseaseComponent implements OnInit {

  public content: any = {
    title: '',
    name: "Diseases",
    data: [],
    fields: [],
    errorMessage: "",
    apiLink: '/diseases',
    caller: '',
    Diseases: {
      diseaseTypes: [],
      diseaseType: {
      },
      symptoms: [
        {
          subSymptoms: [
            {
            }
          ]
        }
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

  get() {
    this.apiService.getData('/diseases', null)
      .subscribe((res: any) => {
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.length <= 0) {
            this.content.errorMessage = "No Disease list found. Click 'Add New' to add.";
          }
          this.content.data = res;
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }


  create() {
    this.content.title = `Add ${this.content.name}`;
    this.content.caller = 'POST';
    this.content.Diseases = {
      diseaseType: {
      },
      symptoms: [
        {
          subSymptoms: [
            {
            }
          ]
        }
      ]
    };

    this.getDiseaseType();

  }

  private getDiseaseType() {
    this.apiService.getData('/diseases/category', null)
      .subscribe((res: any) => {

        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.length <= 0) {
            this.content.errorMessage = "No Disease Type list found. Click 'Add New' to add.";
          }
          this.content.Diseases.diseaseTypes = res; 
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later.";
      });
  }

  drop(event: any, diseaseData: any) {
    moveItemInArray(this.content.data, event.previousIndex, event.currentIndex);

    diseaseData.forEach(function (_data: any, index: number) {
      _data.listingOrder = index
    });
    console.log(diseaseData);

    this.apiService.postData('/diseases/updateListingOrder', diseaseData).subscribe((res: any) => {
      //
    })
  }


  edit(item: any) {
    this.content.title = `Edit ${this.content.name}`;
    this.content.caller = 'PUT';

    this.getDiseaseType();
    this.content.Diseases = item;

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
