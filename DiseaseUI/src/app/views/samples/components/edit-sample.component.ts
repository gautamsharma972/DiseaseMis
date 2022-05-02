import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {HotToastService} from '@ngneat/hot-toast';
import {ApiService} from '../../../_services/api.service';

@Component({
  selector: 'edit-sample-view',
  templateUrl: './edit-sample.component.html'
})
export class EditSampleComponent implements OnInit {

  @Input() content: any = {};
  isSaveLoading: boolean = false;

  @ViewChild('SamplesModal') sampleModal: any;

  constructor(private apiService: ApiService,
              private toast: HotToastService, private router: Router) {
  }

  ngOnInit(): void {
  }

  save() {
    this.isSaveLoading = true;
    if (this.content.Samples.name == undefined) {
      this.toast.error("Name is Empty!!", {
        position: 'top-right',
        autoClose: true
      });
      this.isSaveLoading = false;
    } else {
      this.OperateAPICall(this.content.Samples, this.sampleModal);
    }
  }

  addSubTestType(test: any) {
    test.subTests.push({name: ''});
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
        }
      }, error => {
        this.isSaveLoading = false;
        this.toast.error("Error saving, try again after sometime.", {
          position: 'top-right',
          autoClose: true
        });
      });
    } else {
      postData.id = this.content.Samples.id;
      this.apiService.putData(`${this.content.apiLink}/${this.content.Samples.id}`, this.content.Samples.id, postData).subscribe((res: any) => {
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

  addField() {
    var _subTests: any = [];
    _subTests.push({name: null});
    var obj: any = {name: null, subTests: _subTests};
    this.content.Samples.testTypes.push(obj);
  }

  deleteField(testType: number, index: number, subIndex: number) {
    if (testType == 1) {
      if (index > -1) {
        this.content.Samples.testTypes.splice(index, 1);
      }
    } else if (testType == 2) {
      if (index > -1) {
        this.content.Samples.testTypes[index].subTests.splice(subIndex, 1);
      }
    }
  }
}
