import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { appsettings } from '../settings/appsettings';
import { Employee } from '../models/Employee';
import { ResponseApi } from '../models/ResponseApi';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private http = inject(HttpClient);
  private apiUrl:string = appsettings.apiUrl + "Employee";

  constructor() { }

  List(){
    return this.http.get<Employee[]>(this.apiUrl);
  }

  Get(id:number){
    return this.http.get<Employee>(`${this.apiUrl}/${id}`);
  }

  Create(object:Employee){
    return this.http.post<ResponseApi>(this.apiUrl, object);
  }

  Update(object:Employee){
    return this.http.put<ResponseApi>(this.apiUrl, object);
  }

  Delete(id:number){
    return this.http.delete<ResponseApi>(`${this.apiUrl}/${id}`);
  }
}
