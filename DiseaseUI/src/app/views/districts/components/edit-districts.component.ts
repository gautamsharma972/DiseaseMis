import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { ApiService } from '../../../_services/api.service';
import { DistrictsComponent } from '../districts.component';

@Component({
  selector: 'edit-districts-view',
  templateUrl: './edit-districts.component.html',
  providers: [DistrictsComponent]
})
export class EditDistrictsComponent implements OnInit {

  @Input() content: any = {};
  isSaveLoading: boolean = false;

  @ViewChild('DistrictsModal') districtModal: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService,
    private router: Router,
    private districtsComponent: DistrictsComponent) {
  }

  ngOnInit(): void {
  }

  saveDistrict() {
    this.isSaveLoading = true
    //if (this.content.Districts.name == undefined) {
    //  this.toast.error("Name Field Empty!! Please Enter a Valid Name!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    //} else {
    this.OperateAPICall(this.content.Districts, this.districtModal);
    this.router.navigate([this.router.url])
    //  }
  }

  private OperateAPICall(postData: any, modal: any) {
    if (this.content.caller == 'POST') {
      this.apiService.postData(this.content.apiLink, [postData]).subscribe((res: any) => {
        this.isSaveLoading = false;
        if (res) {
          this.toast.success("New item added", {
            position: 'top-right',
            autoClose: true
          });
          this.districtsComponent.get();
          modal.nativeElement.checked = false;

        }
      }, error => {
        this.isSaveLoading = false;
        this.toast.error("Error saving, try again after sometime.", {
          position: 'top-right',
          autoClose: true
        });
      });
    } else {
      postData.id = this.content.Districts.id;
      this.apiService.putData(`${this.content.apiLink}/${this.content.Districts.id}`, this.content.Districts.id, postData).subscribe((res: any) => {
        this.isSaveLoading = false;
        if (res) {
          this.toast.success("Saved successfully.", {
            position: 'top-right',
            autoClose: true
          });
          modal.nativeElement.checked = false;
          this.districtsComponent.get();
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
