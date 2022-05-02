import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { ApiService } from '../../../_services/api.service';
import { AuthenticationService } from '../../../_services/authentication.service';
declare var $: any;

@Component({
  selector: 'app-entry-form',
  templateUrl: './entry-form.component.html',
  styleUrls: ['./entry-form.component.css']
})
export class EntryFormComponent implements OnInit {

  public content: any = {
    name: "Animal Disease Incidence Report",
    errors: [],
    animals: [{
      symptoms: []
    }],
    diseases: [],
    fixedColumnHeader: [],
    columnHeader: [],
    forms: {
      district: {},
      institute: {},
      incharge: {},
      diseases: []
    },
    districts: [],
    institutes: [],
    incharges: [],
    apiLink: '/forms',
    fileLocked: false,
    formName: "NonInfectious",
    user: {}
  }

  saveFormTitle: any = 'Save and Continue';
  loading: boolean = false;
  formType: string = "Incidence";
  openTab = "incidence";

  constructor(private apiService: ApiService,
    private toast: HotToastService,
    private authService: AuthenticationService,
    private datePipe: DatePipe,
    private currentRoute: ActivatedRoute) {
    this.content.user = this.authService.userValue;
  }

  ngOnInit() {
    this.currentRoute.params.subscribe((param: any) => {
      this.content.formName = param.formName.replace("-", "");
      if (param.formName != "infectious") {
        this.content.FormsType_Incidence = 3;
        this.content.FormsType_Mortality = 4;
      }
      else {
        this.content.FormsType_Incidence = 1;
        this.content.FormsType_Mortality = 2;
      }
      this.getBaseData();
      this.getAnimals();
      this.getDiseases();
      this.getIncidenceForm(false);

    })

  }

  approveEdit() {
    this.apiService.putData(`/forms/${this.content.forms.id}/toggleDiseaseFormLock`,
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
          this.getIncidenceForm(false);
        }
      }, error => {
        this.loading = false;
        this.content.errors.push("Could not load data at this time. Try again later.");
      });
  }

  getIncidenceForm(isAfterSaveLoad: boolean) {
    let $this = this;
    this.loading = true;
    this.apiService.postData(`/forms/get`,
      {
        createdDate: new Date(this.content.forms.createdDate),
        formType: this.content.FormsType_Incidence,
        formName: this.content.formName
      }).subscribe((res: any) => {
        this.loading = false;
        if (res != null) {

          this.content.forms = res;
          let timer = 0;
          $this.content.forms.formDiseaseValues.forEach(function (fd: any) {
            setTimeout(() => {
              if ($(`#animal_${fd.symptom.id}_${fd.animal.id}`).length <= 0) {
                timer++;
              }
              else {
                $(`#animal_${fd.symptom.id}_${fd.animal.id}`).val(fd.value);
                clearTimeout();
              }
            }, timer)

          });

          if (isAfterSaveLoad) {
            if (this.content.forms.currentStep == "incidence") {
              this.openTab = "mortality"
            }
            else if (this.content.forms.currentStep == "mortality") {
              this.saveFormTitle = "Submit";
              this.openTab = "remarks";
            }
            else if (this.content.forms.currentStep == "remarks" ||
              this.content.forms.currentStep == "completed") {
              this.openTab = "completed"

            }
          }
          this.content.fileLocked = this.content.forms.isLocked;

          this.getSelectedValues();
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

    this.content.institutes = [] = this.content.districts.filter((a: any) =>
      a.id == this.content.forms.district.id)[0].institutes;

    this.content.incharges = [] = this.content.institutes.filter((a: any) =>
      a.id == this.content.forms.institute.id)[0].incharges;

    this.content.forms.createdDate = this.datePipe.transform(this.content.forms.createdDate, "yyyy-MM");
  }

  async getMortalityForm() {
    let $this = this;
    this.loading = true;
    await this.apiService.postData(`/forms/get`,
      {
        createdDate: new Date(this.content.forms.createdDate),
        formType: this.content.FormsType_Mortality,
        formName: this.content.formName
      }).subscribe((res: any) => {
        this.loading = false;
        if (res != null) {

          this.content.forms = res;
          let timer = 0;
          $this.content.forms.formDiseaseValues.forEach(function (fd: any) {
            setTimeout(() => {
              if ($(`#animal_${fd.symptom.id}_${fd.animal.id}`).length <= 0) {
                timer++;
              }
              else {
                $(`#animal_${fd.symptom.id}_${fd.animal.id}`).val(fd.value);
                clearTimeout();
              }
            }, timer)

          });

          this.getSelectedValues();
        }
      }, error => {
        this.loading = false;
      })
  }

  calcTotal(symptom: any) {
    var sum = 0;
    $(`.animal_${symptom.id}`).each(function () {
      // @ts-ignore
      sum += +$(this).val();
    });
    $(`.animal_${symptom.id}_total`).val(sum);

  }

  async getAnimals() {

    this.loading = true;
    await this.apiService.getData(`/master/animals`, null)
      .subscribe((res: any) => {

        this.loading = false;
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        } else {
          if (res.length <= 0) {
            this.content.errors.push("No animals found.");
          }
          this.content.animals = [] = res;
          this.content.animals.forEach(function (animal: any) {
            animal.symptoms = [];
            animal.disease = {};
          })
        }
      }, error => {
        this.loading = false;
        this.content.errors.push("Could not load data at this time. Try again later.");
      });
  }

  async getDiseases() {
    this.loading = true;
    await this.apiService.getData(`/diseases/byCategory/${this.content.formName == 'infectious' ? 2 : 1}`, null)
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
            this.content.errors.push("No Diseases found!!");
          }

          res.forEach(function (disease: any) {
            disease.symptoms.forEach(function (_sym: any) {
              _sym.total = 0;
              _sym.subSymptoms.forEach(function (_sub: any) {
                _sub.total = 0;
              })
            })
          })

          this.content.diseases = [] = res;
          this.getColumnHeaders();

        }
      },
        error => {
          this.loading = false;
          this.content.errors.push("Could not load data at this time. Try again later.");
        });
  }

  getFixedHeaders() {
    let flag = 0;
    this.content.diseases.forEach((disease: any) => {
      disease.symptoms.forEach((symptom: any) => {
        symptom.subSymptoms.forEach((subSymptom: any) => {
          if (subSymptom.name != null && flag == 0) {
            this.content.fixedColumnHeader.push("Sub Symptoms");
            flag = 1;
          }
        })
      })
    })
  }

  getColumnHeaders() {
    this.content.fixedColumnHeader = [];
    this.content.columnHeader = [];
    if (this.content.diseases.length > 0) {
      if (this.content.fixedColumnHeader.filter((a: any) => a == "Diseases").length <= 0)
        this.content.fixedColumnHeader.push("Diseases");
      this.content.diseases.every((disease: any) => {
        if (disease.symptoms.length > 0) {
          if (this.content.fixedColumnHeader.filter((a: any) => a == "Symptoms").length <= 0)
            this.content.fixedColumnHeader.push("Symptoms");
          return false;
        }
        return true;
      })
    }
    this.getFixedHeaders();
    this.content.animals.forEach((animal: any) => {
      if (this.content.fixedColumnHeader.filter((a: any) => a == animal.name).length <= 0)
        this.content.columnHeader.push(animal.name);
    });
    if (this.content.fixedColumnHeader.filter((a: any) => a == "Total").length <= 0)
      this.content.columnHeader.push("Total");
  }

  save() {
    let $this = this;
    this.loading = false;
    this.content.forms.animals = this.content.animals;

    var _obj: any = this.createPostObject($this);

    this.apiService.postData(this.content.apiLink, _obj).subscribe((res: any) => {
      this.loading = false;
      if (res) {
        this.getIncidenceForm(true);
      }
    })
  }

  private createPostObject($this: this) {
    var _obj: any = {
      formType: this.openTab == 'incidence'
        ? this.content.FormsType_Incidence :
        this.openTab == 'mortality'
          ? this.content.FormsType_Mortality : this.content.FormsType_Incidence,
      createdDate: this.content.forms.createdDate,
      district: this.content.forms.district,
      institute: this.content.forms.institute,
      incharge: this.content.forms.incharge,
      animals: this.content.animals,
      currentStep: this.openTab,
      remarks: this.content.forms.remarks,
      name: this.content.formName
    };

    _obj.animals.forEach(function (_animal: any) {
      _animal.symptoms = [];
      $this.content.diseases.forEach(function (disease: any) {
        disease.symptoms.forEach(function (symptom: any) {
          var _obj: any = {
            symptom: symptom,
            value: $(`#animal_${symptom.id}_${_animal.id}`).val()
          };
          _obj.symptom.disease = {};
          $.extend(_obj.symptom.disease, disease);
          delete _obj.symptom.disease.symptoms;
          _animal.symptoms.push(_obj);
        });
      });
    });
    return _obj;
  }

  getBaseData() {
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
            this.content.errorMessage = "No Institutes found. Click 'Add New' to add.";
          }
          this.content.districts = res.data;
        }
      }, error => {
        this.content.errorMessage = "Could not load data at this time. Try again later."
      });
  }

  changeDistrict(event: any) {
    this.content.institutes = [] = this.content.districts.filter((a: any) =>
      a.id == event.target.value)[0].institutes;
  }

  changeInstitute(event: any) {
    this.content.incharges = [] = this.content.institutes.filter((a: any) =>
      a.id == event.target.value)[0].incharges;
  }
}


