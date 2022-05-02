import { OnInit } from "@angular/core";
import { Component, AfterViewInit, ViewChild, ElementRef } from "@angular/core";
import { createPopper } from "@popperjs/core";
import { Observable } from "rxjs";
import { User } from "../../../_models/user";
import { AuthenticationService } from "../../../_services/authentication.service";

@Component({
  selector: "app-user-dropdown",
  templateUrl: "./user-dropdown.component.html",
})
export class UserDropdownComponent implements AfterViewInit {
   
  public user: any;

  constructor(private authService: AuthenticationService) {
    
  }
  ngOnInit() {
    var _user: any = this.authService.user.source;
    this.user = _user._value;
  }
    
  ngAfterViewInit() {    
  }

  logout() {
    this.authService.logout();
  } 
}
