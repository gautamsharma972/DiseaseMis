import { Component, OnInit } from '@angular/core';
import { ApiService } from "../../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";
import { DatePipe } from '@angular/common';
import { AfterViewInit } from '@angular/core';
import { AuthenticationService } from '../../../_services/authentication.service';
declare var $: any;

@Component({
  selector: 'app-laboratory-examination',
  templateUrl: './laboratory-examination.component.html',
  styleUrls: ['./laboratory-examination.component.css']
})
export class LaboratoryExaminationComponent implements OnInit, AfterViewInit {

  public content: any = {
    name: "Laboratory Examination Form",
    errors: [],
    categories: [],
    samples: [],
    forms: {
      district: {}
    },
    fixedColumnHeader: [],
    columnHeader: [],
    apiLink: '/forms/laboratory',
    user: {},
    fileLocked: false
  }
  loading: boolean = false;

  constructor(private apiService: ApiService,
    private toast: HotToastService,
    private datePipe: DatePipe,
    private authService: AuthenticationService) {
    this.content.user = this.authService.userValue;
  }

  async ngOnInit(): Promise<void> {
    await this.getDistricts();
    await this.getAnimalCategories();
    await this.getSamples();
  }

  async ngAfterViewInit() {
    await this.getForm()
  }

  async getDistricts() {
    this.loading = true;
    await this.apiService.getData('/master/districts', null)
      .subscribe((res: any) => {
        this.loading = false;
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
          this.content.districts = res.data;
        }
      }, error => {
        this.loading = false;
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }

  async getAnimalCategories() {
    this.loading = true;
    await this.apiService.getData(`/master/animals/category`, null)
      .subscribe((res: any) => {
        this.loading = false;
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        } else {
          if (res.length <= 0) {
            this.content.errors.push("No animal categories found.");
          }

          this.content.categories = res;
          this.content.categories.forEach(function (_cat: any) {
            _cat.subTests = [];
          })
        }
      }, error => {
        this.loading = false;
        this.content.errors.push("Could not load data at this time. Try again later.");
      });
  }

  approveEdit() {
    this.apiService.putData(`/forms/${this.content.forms.id}/toggleLabFormLock`,
      this.content.forms.id, null)
      .subscribe((res: any) => {

        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        } else {
          this.toast.success("Form is unlocked, user can re-edit the form again.", {
            position: 'top-right',
            autoClose: true
          });
          this.getForm();
        }
      }, error => {
        this.loading = false;
        this.content.errors.push("Could not load data at this time. Try again later.");
      });
  }

  async getForm() {
    this.loading = true;
    await this.apiService.postData(`/forms/laboratory/get`,
      {
        createdDate: new Date(this.content.forms.createdDate),
        formType: this.content.FormsType_Incidence,
        formName: this.content.formName
      }).subscribe((res: any) => {
        this.loading = false;
        if (res != null) {

          this.content.forms.createdDate = this.datePipe.transform(res.createdDate, "yyyy-MM");
          this.content.forms.district.id = res.district.id;
          this.content.forms.id = res.id;

          let timer = 0;
          res.labFormValues.forEach(function (fd: any) {
            setTimeout(() => {
              if ($(`#animal_${fd.testTypes.id}_${fd.animalCategory.id}_noOfCase`).length <= 0) {
                timer++;
              }
              else {
                $(`#animal_${fd.testTypes.id}_${fd.animalCategory.id}_total`).val(fd.totalValue);
                $(`#animal_${fd.testTypes.id}_${fd.animalCategory.id}_noOfCase`).val(fd.noOfPositiveCases)
                clearTimeout();
              }
            }, timer)

          });

          if (this.content.forms.isLocked) {
            this.content.fileLocked = true;
          }
          this.getSelectedValues();

          this.content.fileLocked = res.isLocked;

        }
        else {
          this.content.forms.currentStep = null;
          this.content.fileLocked = false;
        }
      }, error => {
        this.loading = false;
      });
  }

  getSelectedValues() {
    var timer = 1;
    if (!this.content.forms.district) {
      setTimeout(() => {
        if (this.content.forms.district) {
          clearTimeout();
        }
        else {
          timer++;
        }
      }, timer);
    }

    this.content.forms.createdDate = this.datePipe.transform(this.content.forms.createdDate, "yyyy-MM");
  }

  async getSamples() {
    this.loading = true;
    await this.apiService.getData(`/samples`, null)
      .subscribe((res: any) => {
        this.loading = false;
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.length <= 0) {
            this.content.errors.push("No Samples test cases found. Try again after sometime.");
          }

          res.forEach(function (samples: any) {
            samples.testTypes.forEach(function (testCase: any) {
              testCase.subTests.forEach(function (_sub: any) {
                _sub.total = 0;
                _sub.totalNoOfveCases = 0;
              })
            })
          })

          this.content.samples = [] = res;

          this.getColumnHeaders();
        }
      }, error => {
        this.loading = false;
        this.content.errors.push("Could not load data at this time. Try again later.");
      });
  }

  getFixedHeaders() {
    let flag = 0;
    this.content.samples.forEach((sample: any) => {
      sample.testTypes.forEach((testType1: any) => {
        testType1.subTests.forEach((testType2: any) => {
          if (testType2.name != null && flag == 0) {
            this.content.fixedColumnHeader.push("Test Type 2");
            flag = 1;
          }
        })
      })
    })
  }

  getColumnHeaders() {
    if (this.content.samples.length > 0) {
      this.content.fixedColumnHeader.push("Samples");
      this.content.samples.every((sample: any) => {
        if (sample.testTypes.length > 0) {
          this.content.fixedColumnHeader.push("Test Type 1");
          return false;
        }
        return true
      })
    }
    this.getFixedHeaders();
    this.content.categories.forEach((category: any) => {
      this.content.columnHeader.push(category.name);
    });
    this.content.columnHeader.push("Total");
  }

  save() {
    this.loading = true;
    let $this = this;
    this.loading = false;
    this.content.forms.animals = this.content.animals;
    var _obj: any = this.createPostObject($this);
    this.apiService.postData(this.content.apiLink, _obj).subscribe((res: any) => {
      this.loading = false;
      if (res) {
        this.getForm();
      }
    }, error => {
      this.loading = false;
    })
  }

  private createPostObject($this: this) {
    var _obj: any = {
      createdDate: this.content.forms.createdDate,
      district: this.content.forms.district,
      samples: this.content.samples,
      remarks: this.content.forms.remarks,
      formName: this.content.formName,
      categories: this.content.categories
    };

    _obj.categories.forEach(function (_cat: any) {
      _cat.tests = [];
      $this.content.samples.forEach(function (_sample: any) {
        _sample.testTypes.forEach(function (_testTypes: any) {
          _testTypes.subTests.forEach(function (_subTest: any) {
            var _obj: any = {
              testType: _subTest,
              totalValue: $(`#animal_${_subTest.id}_${_cat.id}_total`).val(),
              noOfPositiveCases: $(`#animal_${_subTest.id}_${_cat.id}_noOfCase`).val()
            };
            _obj.testType.sample = {};
            $.extend(_obj.testType.sample, _sample);
            delete _obj.testType.sample.testTypes;
            _cat.tests.push(_obj);
          })
        })
      })
    })

    return _obj;
  }

}
