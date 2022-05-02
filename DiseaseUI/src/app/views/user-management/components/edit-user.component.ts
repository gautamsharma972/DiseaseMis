import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { ApiService } from '../../../_services/api.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html'
})
export class EditUserComponent implements OnInit {

  @Input() content: any = {};
  isSaveLoading: boolean = false;

  @ViewChild('UserModal') userModal: any;
  @ViewChild('ChangePasswordModal') changePasswordModal: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService,
    private router: Router) { }

  ngOnInit(): void {
  }

  save() {
    this.isSaveLoading = true;
    this.OperateAPICall(this.content.User, this.userModal);
    this.router.navigate([this.router.url])
  }

  savePassword() {
    this.isSaveLoading = true;
    this.apiService.putData(this.content.apiLink, this.content.User.id, this.content.User).subscribe((res: any) => {
      this.isSaveLoading = false;
      //if (res) {
        this.toast.success(`Password updated for user - ${this.content.User.userName}`, {
          position: 'top-right',
          autoClose: true
        });
        this.changePasswordModal.nativeElement.checked = false;
     // }
    }, error => {
      this.isSaveLoading = false;
      this.toast.error("Error saving, try again after sometime.", {
        position: 'top-right',
        autoClose: true
      });
    });
  }

  private OperateAPICall(postData: any, modal: any) {
    if (this.content.caller == 'POST') {
      this.apiService.postData(this.content.apiLink, postData).subscribe((res: any) => {
        this.isSaveLoading = false;
        if (res) {
          this.toast.success("New item added", {
            position: 'top-right',
            autoClose: true
          });
          modal.nativeElement.checked = false;
        }
      }, error => {
        this.isSaveLoading = false;
        this.toast.error("Error saving, try again after sometime.", {
          position: 'top-right',
          autoClose: true
        });
      });
    }
    else {
      postData.id = this.content.User.id;
      this.apiService.putData(`${this.content.apiLink}/${this.content.User.id}`, this.content.User.id, postData).subscribe((res: any) => {
        this.isSaveLoading = false;
        if (res) {
          this.toast.success("Saved successfully.", {
            position: 'top-right',
            autoClose: true
          });
          modal.nativeElement.checked = false;
        }

      }, error => {
        this.isSaveLoading = false;
        this.toast.error("Error saving, try again after sometime.", {
          position: 'top-right',
          autoClose: true
        });
      });
    }
  }
}
