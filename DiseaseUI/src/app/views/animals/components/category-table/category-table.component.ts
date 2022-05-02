import { Component, Input, OnInit } from '@angular/core';
import { ApiService } from "../../../../_services/api.service";
import { HotToastService } from "@ngneat/hot-toast";
import { AnimalsComponent } from '../../animals.component';

@Component({
  selector: 'app-category-table',
  templateUrl: './category-table.component.html',
  styleUrls: ['./category-table.component.css']
})
export class CategoryTableComponent implements OnInit {

  @Input() content: any = {};
  @Input() searchTerm: any;

  constructor(private apiService: ApiService, 
    private toast: HotToastService) {

  }

  ngOnInit(): void {
  }

  setData(content: any) {
    this.content = content;
  }

  edit(item: any) {
    this.content.title = `Edit ${this.content.name}`;
    this.content.caller = 'PUT';
    this.content.Animals = item;
    this.content.typeOf = 'animals';
    this.content.apiLink = '/master/animals/category'
  }

  delete(id: any) {
    this.apiService.deleteData(`/master/animals/category/${id}`, id).subscribe((res: any) => {
      if (res) {
        this.toast.success("Item Deleted", {
          position: 'top-right',
          autoClose: true
        });
         
      }
    }, error => {
      this.toast.error("Error deleting, try again after sometime.", {
        position: 'top-right',
        autoClose: true
      });
    });
  }

}
