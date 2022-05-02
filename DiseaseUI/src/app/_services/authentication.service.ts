import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators'; 
import { environment } from '../../environments/environment';
import { User } from '../_models/user'; 

@Injectable({ providedIn: 'root' })
export class AuthenticationService {

  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;

  constructor(
    private router: Router,
    private http: HttpClient
  ) {
    // @ts-ignore
    this.userSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('loggedUser')));
    this.user = this.userSubject.asObservable();
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  login(userName: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/Auth/Verify`, { userName, password },
      { withCredentials: true })
      .pipe(map(user => {
        
        if (user.error) { 
          return false;
        }
        sessionStorage.setItem("loggedUser", JSON.stringify(user));
        this.userSubject.next(user);
        this.startRefreshTokenTimer();
        return user;
      }));
  }

  logout() {
    window.sessionStorage.clear(); 
    this.stopRefreshTokenTimer();
    // @ts-ignore   
    this.userSubject.next(null);
   
    this.http.post<any>(`${environment.apiUrl}/logout`, {}, { withCredentials: true }).subscribe();
    this.router.navigate(['/login']);
  }

  refreshToken() {
    return this.http.post<any>(`${environment.apiUrl}/Auth/Verify`, {}, { withCredentials: true })
      .pipe(map((user) => {
        this.userSubject.next(user);
        this.startRefreshTokenTimer();
        return user;
      }));
  }

  // helper methods

  // @ts-ignore
  private refreshTokenTimeout: NodeJS.Timeout;

  private startRefreshTokenTimer() {
    // parse json object from base64 encoded jwt token
    // @ts-ignore
    if (this.userValue.access_token != undefined) {
      const jwtToken = this.userValue.access_token.accessToken;

      // set a timeout to refresh the token a minute before it expires
      const expires = new Date(jwtToken.exp * 1000);
      const timeout = expires.getTime() - Date.now() - (60 * 1000);
      this.refreshTokenTimeout = setTimeout(() => this.refreshToken().subscribe(), timeout);
    }
   
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout);
  }
}
