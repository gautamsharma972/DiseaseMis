import { Component, Input, OnInit } from '@angular/core';
import { ApiService } from "../../../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { AnimalsComponent } from '../../animals.component';

@Component({
  selector: 'app-animal-table',
  templateUrl: './animal-table.component.html',
  styleUrls: ['./animal-table.component.css']
})
export class AnimalTableComponent implements OnInit {

  @Input() content: any;
  @Input() searchTerm: any;

  constructor(private apiService: ApiService, 
    private toast: HotToastService) { }

  ngOnInit(): void {
  }

  setData(content: any) {
    this.content = content;
  }

  drop(event: any, updateAnimalList: any) {
    moveItemInArray(this.content.animals, event.previousIndex, event.currentIndex);

    updateAnimalList.forEach(function (_animal: any, index: number) {
      _animal.listingOrder = index
    });
    console.log(updateAnimalList);

    this.apiService.postData('/master/animals/updateListingOrder', updateAnimalList).subscribe((res: any) => {
      //
    })
  }


  edit(item: any) {
    this.content.title = `Edit ${this.content.name}`;
    this.content.caller = 'PUT';
    this.content.Animals = item;
    this.content.typeOf = 'animals';
    this.content.apiLink = '/master/animals';
  }

  delete(id: any) {
    this.apiService.deleteData(`/master/animals/${id}`, id).subscribe((res: any) => {
      
    }, error => {
      this.toast.error("Error deleting, try again after sometime.", {
        position: 'top-right',
        autoClose: true
      });
    });
  }

}
