import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {HotToastService} from '@ngneat/hot-toast';
import {ApiService} from '../../../_services/api.service';

@Component({
  selector: 'edit-institutes-view',
  templateUrl: './edit-institutes.component.html'
})
export class EditInstitutesComponent implements OnInit {

  @Input() content: any = {};
  isSaveLoading: boolean = false;

  @ViewChild('InstitutesModal') institutesModal: any;

  constructor(private apiService: ApiService,
              private toast: HotToastService, private router: Router) {
  }

  ngOnInit(): void {
  }

  save() {
    this.isSaveLoading = true;
    //if (this.content.Institutes.name == undefined && this.content.Institutes.district.id == undefined) {
    //  this.toast.error("Name and District Field Empty!! Enter Name and District to save!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    //}
    //else if(this.content.Institutes.name == undefined) {
    //  this.toast.error("Name Field Empty!! Enter Name to save!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    //}
    //else if(this.content.Institutes.district.id == undefined) {
    //  this.toast.error("District Field Empty!! Enter District to save!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    //}
    //else {
      this.OperateAPICall(this.content[this.content.name], this.institutesModal);
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
      postData.id = this.content.Institutes.id;
      this.apiService.putData(`${this.content.apiLink}/${this.content.Institutes.id}`, this.content.Institutes.id, postData).subscribe((res: any) => {
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
