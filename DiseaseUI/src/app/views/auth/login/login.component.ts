import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Router } from '@angular/router';

import { AuthenticationService } from "../../../_services/authentication.service";
import { HotToastService } from "@ngneat/hot-toast";

interface IUser {
  userName: string;
  password: string;
  showPassword: boolean;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  user: IUser;
  isLoggingIn: boolean = false;

  constructor(private authenticationService: AuthenticationService,
    private _router: Router, private toast: HotToastService) {
    this.user = {} as IUser;
  }

  public validate(form: NgForm): void {
    if (form.invalid) {
      for (const control of Object.keys(form.controls)) {
        form.controls[control].markAsTouched();
      }
      return;
    }
    else {
      this.isLoggingIn = true;
      this.authenticationService.login(this.user.userName, this.user.password)
        .subscribe((res: any) => {
          debugger;
          this.isLoggingIn = false;
          if (!res) {
            this.toast.error("Invalid login attempt. Please try again", {
              position: 'top-right',
              autoClose: true
            });

          }
          else {
            if (this.authenticationService.userValue.userRoles.includes('Laboratory-Level')) {
              this._router.navigate(["mis/laboratory"]);
            }
            else if (this.authenticationService.userValue.userRoles.includes('District-Level')) {
              this._router.navigate(["mis/forms/non-infectious"]);
            }
            else {
              this._router.navigate(["mis"]);
            }
          }

        }, error => {
          this.isLoggingIn = false;
          this.toast.error("Invalid login attempt. Please try again", {
            position: 'top-right',
            autoClose: true

          });
          return false;
        })
    }
  }

}
