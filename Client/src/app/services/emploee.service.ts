import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Employee } from '../entities/emploee';

@Injectable({ providedIn: 'root' })
export class EmployeeService{
    
    private url = "https://localhost:44347/api/employee";
    constructor(private http: HttpClient){ }
       
    getEmployees(){
        return this.http.get(this.url+"/GetAllEmployees");
    }
   
    getEmployeesByDepartment(departmentId:number)
    {
        return this.http.get(this.url+"/GetEmployeesByDepartment?departmentId="+departmentId);
    }

    createEmployee(employee: Employee){
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.post(this.url+"/CreateEmployee", JSON.stringify(employee), {headers: myHeaders}); 
    }
    updateEmployee(employee: Employee) {
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.put(this.url+"/UpdateEmployee/"+employee.id, JSON.stringify(employee), {headers:myHeaders});
    }
    deleteEmployee(id: number){
        return this.http.delete(this.url + '/DeleteEmployee/' + id);
    }
}