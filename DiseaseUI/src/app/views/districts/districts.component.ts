import { Component, OnInit } from '@angular/core';
import { ApiService } from "../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";
import { Router } from "@angular/router";

@Component({
  selector: 'app-districts',
  templateUrl: './districts.component.html',
  styleUrls: ['./districts.component.css']
})
export class DistrictsComponent implements OnInit {

  public content: any = {
    title: '',
    name: "Districts",
    data: [],
    fields: [],
    errorMessage: "",
    apiLink: '/master/districts',
    caller: '',
    Districts: {}
  }

  searchTerm: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.apiService.getData('/master/districts', null)
      .subscribe((res: any) => {
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.data.length <= 0) {
            this.content.errorMessage = "No Districts found. Click 'Add New' to add.";
          }
          this.content.data = res.data;
          this.content.fields = res.fields;
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }

  create() {
    this.content.title = `Add ${this.content.name}`;
    this.content.caller = 'POST';
    this.content.Districts = {};
  }

  edit(item: any) {
    this.content.title = `Edit ${this.content.name}`;
    this.content.caller = 'PUT';
    this.content.Districts = item;
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
