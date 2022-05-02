import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { ApiService } from '../../_services/api.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  public content: any = {
    title: '',
    name: "User",
    data: [],
    archivedRecords: [],
    allRecords: [],
    fields: [],
    errorMessage: "",
    apiLink: '/auth',
    caller: '',
    User: {},
    archivedTitle: 'Archived',
    requiredMapping: {
      link: '/auth/roles',
      title: 'Roles',
      name: 'RoleId',
      mappingData: []
    }
  }

  public searchTerm: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.apiService.getData(`/user`, null)
      .subscribe((res: any) => {
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.length <= 0) {
            this.content.errorMessage = "No Users found. Click 'Add New' to add.";
          }

          this.content.data = res;
          this.content.archivedRecords = res.filter((a: any) => a.isActive = !a.isActive);
          this.content.allRecords = res;
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }

  create() {
    this.content.title = `Add ${this.content.name}`;
    this.content.caller = 'POST';
    this.content.User = {};
    this.apiService.getData(this.content.requiredMapping.link, null).subscribe((res: any) => {
      this.content.requiredMapping.mappingData = res.data;
    });
    this.content.apiLink = "/auth/register";
  }

  edit(item: any) {
    this.content.title = `Edit ${this.content.name}`;
    this.content.caller = 'PUT';
    this.apiService.getData(this.content.requiredMapping.link, null).subscribe((res: any) => {
      this.content.requiredMapping.mappingData = res.data;
    })
    this.content.User = item;
    this.content.User.role = item.userRoles == null ? '' : item.userRoles[0];
    this.content.apiLink = "/auth/users"; 
  }

  changePassword(item: any) {
    this.content.User = item;
    this.content.apiLink = `/auth/users/${item.id}/resetPassword`
  }

  toggleArchiveList() {
    this.content.data = this.content.archivedRecords;
  }

  viewAll() {
    this.content.data = this.content.allRecords;
  }

  delete(id: any) {
    this.apiService.deleteData(`/user/toggleStatus/${id}`, id).subscribe((res: any) => {
      this.get();
      return;
    }, error => {
      this.toast.error("Error archiving, try again after sometime.", {
        position: 'top-right',
        autoClose: true
      });
    });
  }
}
