import { Component, OnInit } from '@angular/core';
import {navItems} from "../../_nav";

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit {

  navItems: Array<any>;
  constructor() {
    this.navItems = navItems;
  }

  ngOnInit(): void {
  }

}
