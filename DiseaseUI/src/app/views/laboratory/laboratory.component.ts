import { Component, OnInit } from '@angular/core';
import { ApiService } from "../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";

@Component({
  selector: 'app-laboratory',
  templateUrl: './laboratory.component.html',
  styleUrls: ['./laboratory.component.css']
})
export class LaboratoryComponent implements OnInit {
  public content: any = {
    name: "Laboratories",
    data: [],
    fields: [],
    errorMessage: "",
    apiLink: '/master/laboratories',
    caller: '',
    requiredMapping: {
      link: '/master/districts',
      title: 'District',
      name: 'DistrictId',
      fieldType: 'dropdown',
      mappingData: []
    },
    Laboratories: {
      districts: {}
    }
  }
  searchTerm: any;


  constructor(private apiService: ApiService,
    private toast: HotToastService) { }

  ngOnInit(): void {
    this.get();
  }

  private get() {
    this.apiService.getData('/master/laboratories', null)
      .subscribe((res: any) => {
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.data.length <= 0) {
            this.content.errorMessage = "No Laboratories found. Click 'Add New' to add.";
          }

          this.content.data = res.data;
          this.content.fields = res.fields;
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }

  create() {
    this.content.Laboratories = {
      districts: {}
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

    this.content.Laboratories = item;
  }

  delete(id: any) {
    this.apiService.deleteData(`${this.content.apiLink}/${id}`, id).subscribe((res: any) => {
      return;
    }, error => {
      this.toast.error("Error deleting, try again after sometime.", {
        position: 'top-right',
        autoClose: true
      });
    });
  }
}
