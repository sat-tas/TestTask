import { EmployeeService } from './emploee.service';
import { Employee } from '../entities/emploee';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
  })
export class DataService{
    public employees : Array<Employee>=new Array<Employee>();;
    constructor(private employeeService: EmployeeService){ 
        
    }
}