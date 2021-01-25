import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpResponse} from '@angular/common/http';
import { Department } from '../entities/department';

    
@Injectable({ providedIn: 'root' })
export class DepartmentService{
    
    private url = "https://localhost:44347/api/department";
    


    constructor(private http: HttpClient){ }
       
    getPagedDepartments(pageNumber:number,searchTerm:string){
        return this.http.get(this.url+"/GetDepartments?PageNumber="+pageNumber+"&searchTerm="+searchTerm,{observe: 'response'});
    }

    getDepartments(){
        return this.http.get(this.url+"/GetAllDepartments");
    }


    createDepartment(department: Department){
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.post(this.url+"/CreateDepartment", JSON.stringify(department), {headers: myHeaders}); 
    }
    updateDepartment(department: Department) {
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.put(this.url+"/UpdateDepartment/"+department.id, JSON.stringify(department), {headers:myHeaders});
    }
    deleteDepartment(id: number){
        return this.http.delete(this.url + '/DeleteDepartment/' + id);
    }
}