import { Component, OnInit } from '@angular/core';
import { ApiService } from "../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";

@Component({
  selector: 'app-in-charge',
  templateUrl: './in-charge.component.html',
  styleUrls: ['./in-charge.component.css']
})
export class InChargeComponent implements OnInit {
  public content: any = {
    title: '',
    name: "Incharge",
    data: [],
    fields: [],
    errorMessage: "",
    archivedRecords: [],
    allRecords: [],
    archivedTitle: 'Archived',
    apiLink: '/master/incharges',
    caller: '',
    requiredMapping: {
      link: '/master/institutes',
      title: 'Institute',
      name: 'InstituteId',
      fieldType: 'dropdown',
      mappingData: []
    },
    Incharge: {
      institute: {}
    }
  }

  searchTerm: any;
  constructor(private apiService: ApiService, private toast: HotToastService) { }

  ngOnInit(): void {
    this.get();
  }

  private get() {
    this.apiService.getData('/master/incharges', null)
      .subscribe((res: any) => {
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.data.length <= 0) {
            this.content.errorMessage = "No In-charges found. Click 'Add New' to add.";
          } 
          this.content.data = res.data;
          this.content.archivedRecords = res.data.filter((a: any) => a.status == 1);
          this.content.allRecords = res.data;
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }


  create() {
    this.content.title = `Add ${this.content.name}`;
    this.content.caller = 'POST';

    this.apiService.getData(`/master/institutes`, null).subscribe((res: any) => {
      if (res)
        this.content.mappingData = res.data;
    });

  }

  edit(item: any) {
    this.content.title = `Edit ${this.content.name}`;
    this.content.caller = 'PUT';

    this.apiService.getData(`/master/institutes`, null).subscribe((res: any) => {
      if (res)
        this.content.mappingData = res.data;
    });

    this.content.Incharge = item;
  }

  delete(id: any) {
    this.apiService.deleteData(`${this.content.apiLink}/${id}`, id).subscribe((res: any) => {
      this.get();
      return;
    }, error => {
      this.toast.error("Error archiving, try again after sometime.", {
        position: 'top-right',
        autoClose: true
      });
      return;
    });
  }

  toggleArchiveList() {
    this.content.data = this.content.archivedRecords;
  }

  viewAll() {
    this.content.data = this.content.allRecords;
  }
}
