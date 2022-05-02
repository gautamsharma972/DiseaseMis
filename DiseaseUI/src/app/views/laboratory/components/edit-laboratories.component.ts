import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { ApiService } from '../../../_services/api.service';

@Component({
  selector: 'edit-laboratories-view',
  templateUrl: './edit-laboratories.component.html'
})
export class EditLaboratoriesComponent implements OnInit {

  @Input() content: any = {};
  isSaveLoading: boolean = false;

  @ViewChild('laboratoryModal') laboratoryModal: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService) {
  }

  ngOnInit(): void {
  }

  save() {
    this.isSaveLoading = true;
    //if (this.content.Laboratories.location == undefined && this.content.Laboratories.districts.id == undefined) {
    //  this.toast.error("Fields are Empty!! Fill the Fields to save!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    //} else if (this.content.Laboratories.location == undefined) {
    //  this.toast.error("Location is Empty!!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    //} else if (this.content.Laboratories.districts.id == undefined) {
    //  this.toast.error("Districts is Empty!!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    //} else {
    this.OperateAPICall(this.content.Laboratories, this.laboratoryModal);
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
      postData.id = this.content.Laboratories.id;
      this.apiService.putData(`${this.content.apiLink}/${this.content.Laboratories.id}`, this.content.Laboratories.id, postData).subscribe((res: any) => {
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
