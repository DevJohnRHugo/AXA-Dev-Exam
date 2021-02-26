import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  readonly apiUrl: string = 'https://localhost:44356/api/application/';
    //readonly apiUrl: string = 'https://localhost:4200/api/application/';
  constructor(private http: HttpClient, private toastrService: ToastrService) {
  }

  registerUser(user: any[]) {
    return this.http.post<any>(`${this.apiUrl}register`, user);
  }

  uploadFile(file: Blob) {   
    const formData = new FormData();
    formData.append('fileUpload', file);
    return this.http.post<any>(`${this.apiUrl}upload`, formData);
  }

  scheduleInterview(schedule: any) {   
    return this.http.post<any>(`${this.apiUrl}schedule`, schedule);
  }
}
