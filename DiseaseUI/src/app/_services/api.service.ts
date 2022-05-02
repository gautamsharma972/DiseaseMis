import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Injectable } from "@angular/core";
import { HotToastService } from "@ngneat/hot-toast";
import { error } from '@angular/compiler/src/util';

@Injectable({ providedIn: 'root' })
export class ApiService {

  content: any = {};

  constructor(private http: HttpClient, private toast: HotToastService) {
  }

  getData(url: string, id: any) {
    return this.http.get<any>(`${environment.apiUrl}${url}`, { withCredentials: true })
      .pipe(map(content => {
        if (content == null || content.length <= 0) {
          this.content.errorMessage = "No Data Found!!";
          return content;
        } else {
          this.content.data = content;
          // @ts-ignore
          this.content.fields = Object.keys(content);
          return content;
        }
      }), error => {
        return error;
      });
  }

  postData(url: string, data: any) {
    return this.http.post<any>(`${environment.apiUrl}${url}`, data, { withCredentials: true })
      .pipe(map(content => {

        if (content != null && content.hasOwnProperty('hasError') && content.hasError) {
          if (content.errors[0] == '1062') {
            this.toast.error("Same data already exists. Try again with another name.", {
              position: 'top-right',
              autoClose: true
            });
            return false;
          }
          else {
            this.toast.error(content.errors[0], {
              position: 'top-right',
              autoClose: true
            });
            return false;
          }
        }
        return content;
      }), error => {
        return error;
      });
  }

  putData(url: string, id: any, data: any) {
    return this.http.put<any>(`${environment.apiUrl}${url}`, data, { withCredentials: true })
      .pipe(map(content => {
        if (content != null && content.hasOwnProperty('hasError') && content.hasError) {
          if (content.errors[0] == '1062') {
            this.toast.error("Same data already exists. Try again with another name.", {
              position: 'top-right',
              autoClose: true
            });
            return false;
          }
          else {
            this.toast.error(content.errors[0], {
              position: 'top-right',
              autoClose: true
            });
            return false;
          }
        }
        return content;
      }), error => {
        return error;
      });
  }

  deleteData(url: string, id: any) {
    return this.http.delete<any>(`${environment.apiUrl}${url}`, { withCredentials: true })
      .pipe(map(content => {
        if (content.hasError) {
          this.toast.error(content.errors[0], {
            position: 'top-right',
            autoClose: true
          });
          
        }
        else {
          this.toast.success("Item deleted", {
            position: 'top-right',
            autoClose: true
          });
        }
      }), error => {
        return error;
      });
  }

}
