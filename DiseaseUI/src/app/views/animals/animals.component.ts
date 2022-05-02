import { Component, OnInit } from '@angular/core';
import { ApiService } from "../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";
import { CategoryTableComponent } from './components/category-table/category-table.component';
import { AnimalTableComponent } from './components/animal-table/animal-table.component';
import { Subscription } from 'rxjs';
import { SharedService } from '../../_services/shared.service';

@Component({
  selector: 'app-animals',
  templateUrl: './animals.component.html',
  styleUrls: ['./animals.component.css'],
  providers: [CategoryTableComponent, AnimalTableComponent]
})
export class AnimalsComponent implements OnInit {

  public content: any = {
    title: '',
    name: "Animals",
    data: [],
    fields: [],
    errorMessage_categories: "",
    errorMessage_animals: "",
    apiLink: '/master/animals',
    caller: '',
    Animals: { 
    },
    animals: [],
    categories: []
  }

  searchTerm: any;
  private subscription: Subscription

  constructor(private apiService: ApiService,
    private categoryTable: CategoryTableComponent,
    private animalTable: AnimalTableComponent,
    private sharedService: SharedService,
    private toast: HotToastService) {
    this.subscription = this.sharedService.getLatestData().subscribe
      (data => {
        this.getCategories(true);
        this.getAnimals(true);
      });
  }

  ngOnInit(): void {
    this.getCategories(false);
  }

  getCategories(callService: boolean) {
    this.apiService.getData('/master/animals/category', null)
      .subscribe((res: any) => { 
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.length <= 0) {
            this.content.errorMessage_categories = "No Categories found. Click 'Add New' to add.";
          }
          this.content.categories = res;
          this.categoryTable.setData(this.content);// = res;

          if (callService) {
            this.sharedService.sendLatestData(this.content.categories);
          }
        }
      }, error => {
        this.content.errorMessage_categories = "Could not load data at this time. Try again later."
      });
  }

  getAnimals(callService: boolean) {
    this.apiService.getData('/master/animals', null)
      .subscribe((res: any) => {
         
        if (!res) {
          this.toast.error("Could not connect to Server. Try again after sometime.", {
            position: 'top-right',
            autoClose: true
          });
        }
        else {
          if (res.length <= 0) {
            this.content.errorMessage_animals = "No animals found. Click 'Add New' to add.";
          }
          this.content.animals = res;
          this.animalTable.setData(this.content);// = res;
          if (callService) {
            this.sharedService.sendLatestData(this.content.categories);
          }
        }
      }, error => {
        this.content.errorMessage_animals = "Could not load data at this time. Try again later."
      });
  }

  create(title: string) {
    this.content.title = `Add ${title}`;
    this.content.apiLink = (title == 'animals') ? '/master/animals' : '/master/animals/category';
    this.content.caller = 'POST';
    this.content.Animals = {
      category: {}
    };
    this.content.typeOf = title;
  }

}
