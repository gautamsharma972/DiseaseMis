import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class SharedService {
  private subjectName = new Subject<any>();

  sendLatestData(data: any) {
    this.subjectName.next(data);
  }

  getLatestData(): Observable<any> {
    return this.subjectName.asObservable();
  }
}
