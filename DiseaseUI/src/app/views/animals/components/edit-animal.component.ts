import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {HotToastService} from '@ngneat/hot-toast';
import {ApiService} from '../../../_services/api.service';
import { AnimalsComponent } from '../animals.component';

@Component({
  selector: 'edit-animal-view',
  templateUrl: './edit-animal.component.html'
})
export class EditAnimalComponent implements OnInit {

  @Input() content: any = {};
  isSaveLoading: boolean = false;

  @ViewChild('AnimalsModal') animalModal: any;

  constructor(private apiService: ApiService,
    private toast: HotToastService,
    private router: Router,
    private animalsComponent: AnimalsComponent) {
  }

  ngOnInit(): void {
  }

  save() {
    this.isSaveLoading = true; 
    //if (this.content.Animals.name == undefined || this.content.Animals.category.id == undefined) {
    //  this.isSaveLoading = false;
    //  //this.toast.error("Fields are not fully filled!!", {
    //  //  position: 'top-right',
    //  //  autoClose: true
    //  //});
    //  this.isSaveLoading = false;
    //} else {
      this.OperateAPICall(this.content.Animals, this.animalModal);
  //  }
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

        this.animalsComponent.getCategories(true);
        this.animalsComponent.getAnimals(true);

      }, error => {
        this.isSaveLoading = false;
        this.toast.error("Error saving, try again after sometime.", {
          position: 'top-right',
          autoClose: true
        });
      });
    } else {
      postData.id = this.content.Animals.id;
      this.apiService.putData(`${this.content.apiLink}/${this.content.Animals.id}`, this.content.Animals.id, postData).subscribe((res: any) => {
        this.isSaveLoading = false;
        this.toast.success("Saved successfully.", {
          position: 'top-right',
          autoClose: true
        });
        modal.nativeElement.checked = false;

        this.animalsComponent.getCategories(true);
        this.animalsComponent.getAnimals(true);

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
