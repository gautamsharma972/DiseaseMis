import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../_services/api.service';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-institutions',
  templateUrl: './institutions.component.html',
  styleUrls: ['./institutions.component.css']
})
export class InstitutionsComponent implements OnInit {

  public content: any = {
    name: "Institutes",
    data: [],
    fields: [],
    errorMessage: "",
    apiLink: '/master/institutes',
    caller: '',
    requiredMapping: {
      link: '/master/districts',
      title: 'District',
      name: 'DistrictId',
      fieldType: 'dropdown',
      mappingData: []
    },
    Institutes: {
      district: {}
    }
  }
  searchTerm: any;


  constructor(private apiService: ApiService,
    private toast: HotToastService) { }

  ngOnInit(): void {
    this.get();
  }

  private get() {
    this.apiService.getData('/master/Institutes', null)
      .subscribe((res: any) => {
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.data.length <= 0) {
            this.content.errorMessage = "No Institutes found. Click 'Add New' to add.";
          }

          this.content.data = res.data;
          this.content.fields = res.fields;
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }

  create() {
    this.content.Institutes = {
      district: {}
    }
    this.content.title = `Add ${this.content.name}`;
    this.content.caller = 'POST';
    this.apiService.getData(`/master/districts`, null).subscribe((res: any) => {
      if (res)
        this.content.mappingData = res.data;
    });

  }

  edit(item: any) {
    this.content.title = `Edit ${this.content.name}`;
    this.content.caller = 'PUT';

    this.apiService.getData(`/master/districts`, null).subscribe((res: any) => {
      if (res)
        this.content.mappingData = res.data;
    });

    this.content.Institutes = item;
  }

  delete(id: any) {
    this.apiService.deleteData(`${this.content.apiLink}/${id}`, id).subscribe((res: any) => {
      this.get();
    }, error => {
      this.toast.error("Error deleting, try again after sometime.", {
        position: 'top-right',
        autoClose: true
      });
    });
  }
}
