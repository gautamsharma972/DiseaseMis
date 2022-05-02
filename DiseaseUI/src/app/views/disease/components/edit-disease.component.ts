import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { ApiService } from '../../../_services/api.service';
import { DiseaseComponent } from '../disease.component';

@Component({
  selector: 'edit-disease-view',
  templateUrl: './edit-disease.component.html',
  providers: [DiseaseComponent]
})
export class EditDiseaseComponent implements OnInit {

  @Input() content: any = {};
  isSaveLoading: boolean = false;

  @ViewChild('DiseasesModal') diseaseModal: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService,
    private diseaseComponent: DiseaseComponent) {
  }

  ngOnInit(): void {
  }

  save() {
    this.isSaveLoading = true;
    //if (this.content.Diseases.name == undefined) {
    //  this.toast.error("Name Field Empty!!", {
    //    position: 'top-right',
    //    autoClose: true
    //  });
    //  this.isSaveLoading = false;
    //} else {
     
    //}
    this.OperateAPICall(this.content.Diseases, this.diseaseModal);
  }

  private OperateAPICall(postData: any, modal: any) {
    if (this.content.caller == 'POST') {
      this.apiService.postData(this.content.apiLink, [postData]).subscribe((res: any) => {

        if (res) {
          this.isSaveLoading = false;
          this.toast.success("New item added", {
            position: 'top-right',
            autoClose: true
          });
          modal.nativeElement.checked = false;
          this.diseaseComponent.get();
        }
      }, error => {
        this.isSaveLoading = false;
        this.toast.error("Error saving, try again after sometime.", {
          position: 'top-right',
          autoClose: true
        });
      });
    } else {
      postData.id = this.content.Diseases.id;
      this.apiService.putData(`${this.content.apiLink}/${this.content.Diseases.id}`, this.content.Diseases.id, postData).subscribe((res: any) => {
        this.isSaveLoading = false;
        this.toast.success("Saved successfully.", {
          position: 'top-right',
          autoClose: true
        });
        modal.nativeElement.checked = false;
        this.diseaseComponent.get();
      }, error => {
        this.isSaveLoading = false;
        this.toast.error("Error saving, try again after sometime.", {
          position: 'top-right',
          autoClose: true
        });
      });
    }
  }

  addField() {
    var _subSymptoms: any = [];
    _subSymptoms.push({ name: null });
    var obj: any = { name: null, subSymptoms: _subSymptoms };
    this.content.Diseases.symptoms.push(obj);
  }

  deleteField(index: number) {
    if (index > -1) {
      this.content.Diseases.symptoms.splice(index, 1);
    }
  }
}
