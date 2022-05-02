import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { ApiService } from '../../../_services/api.service';

@Component({
  selector: 'edit-incharges-view',
  templateUrl: './edit-incharges.component.html'
})
export class EditInchargesComponent implements OnInit {

  @Input() content: any = {};
  isSaveLoading: boolean = false;

  @ViewChild('InchargesModal') districtModal: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService, private router: Router) {
  }

  ngOnInit(): void {
  }

  saveIncharge() {
    this.isSaveLoading = true;
    //if (this.content.Incharge.name == undefined && this.content.Incharge.institute.id == undefined
    //  && this.content.Incharge.phone == undefined && this.content.Incharge.designation == undefined
    //) {
    //  this.toast.error("Some Fields are Empty!! Fill all the Fields and save!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    // } else {
    this.OperateAPICall(this.content.Incharge, this.districtModal);
    //}
  }

  private OperateAPICall(postData: any, modal: any) {
    if (this.content.caller == 'POST') {
      this.apiService.postData(this.content.apiLink, [postData]).subscribe((res: any) => {
        this.isSaveLoading = false;
        this.toast.success("New item added", {
          position: 'top-right',
          autoClose: true
        });
        modal.nativeElement.checked = false;

      }, error => {
        this.isSaveLoading = false;
        this.toast.error("Error saving, try again after sometime.", {
          position: 'top-right',
          autoClose: true
        });
      });
    } else {
      postData.id = this.content.Incharge.id;
      this.apiService.putData(`${this.content.apiLink}/${this.content.Incharge.id}`, this.content.Incharge.id, postData).subscribe((res: any) => {
        this.isSaveLoading = false;
        this.toast.success("Saved successfully.", {
          position: 'top-right',
          autoClose: true
        });
        modal.nativeElement.checked = false;
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
