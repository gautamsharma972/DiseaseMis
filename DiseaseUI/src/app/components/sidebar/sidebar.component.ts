import { Component, OnInit } from "@angular/core";
import {navItems} from "../../_nav";
import { AuthenticationService } from "../../_services/authentication.service";

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
})
export class SidebarComponent implements OnInit {

  collapseShow = "hidden";
  user: any;
  navItems: Array<any>;

  constructor(private authService: AuthenticationService) {
    this.navItems = navItems; 
    this.user = authService.userValue;
  }

  ngOnInit() { 
  }

  toggleCollapseShow(classes: any) {
    this.collapseShow = classes;
  }

  logout() {
    this.authService.logout();
  }
}
