import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Department } from '../entities/department';
import { ErrorDetails } from '../entities/errorModel';
import { NgbdModalConfirm } from '../modal/modal.component';
import { DepartmentService } from '../services/department.service';
import { HttpHeaders } from '@angular/common/http';
import { EmployeeComponent } from '../employee/employee.component';
import { DataService } from '../services/data.service';
import { EmployeeService } from '../services/emploee.service';
import { Employee } from '../entities/emploee';
import { flattenDiagnosticMessageText } from 'typescript';
   
@Component({ 
    selector: 'app-home', 
    templateUrl: './department.component.html',
    providers: [DepartmentService,NgbModal],
    styleUrls:['./department.component.css']
}) 
export class DepartmentComponent implements OnInit 
{
    @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>;
    @ViewChild('readOnlyTemplateEmployee', {static: false}) readOnlyTemplateEmployee: TemplateRef<any>;

    @ViewChild('editTemplate', {static: false}) editTemplate: TemplateRef<any>;
    
    editedDepartment: Department;
    departments: Array<Department>=new Array<Department>();
    isNewRecord: boolean;
    statusMessage: string="Output";
    isError:boolean=false;
    pageNumber:number=1;
    pageSize:number;
    pageCount:number;
    searchTerm:string="";
    employees:Array<Employee>=new Array<Employee>();
    openDeprtmentId:number;
    constructor(private serv: DepartmentService,private  servEmployee:EmployeeService,private _modalService: NgbModal,private dataService: DataService) 
    {
    }
       
    ngOnInit() {
        this.loadDepartments(this.pageNumber);
        
    }

    private loadEmployees(id:number) {
        this.servEmployee.getEmployeesByDepartment(id).subscribe((data: Employee[]) => {
            this.employees=this.employees.concat(data); 
            });
    }

    loadDepartments(page : number=1) {
        this.serv.getPagedDepartments(page,this.searchTerm).subscribe(
                resp =>
                {
                    this.departments=<Department[]>resp.body;
                    var res=JSON.parse(resp.headers.get('x-pagination'))
                    this.pageCount=res.TotalCount;
                    this.pageSize=res.PageSize;
                },
                error => 
                {
                    this.statusMessage = error.message;
                }
            );
    }

    searchDepartment()
    {
        this.pageNumber=1;
        this.loadDepartments(this.pageNumber);
    }

    addDepartment() {
        this.editedDepartment = new Department(0,"",null,null);
        this.departments.push(this.editedDepartment);
        this.isNewRecord = true;
    }
    
    editDepartment(department: Department) {
        this.editedDepartment = new Department(department.id, department.name, department.dateAdded,department.dateChangeInfo);
    }

    loadTemplate(department: Department) {
        if (this.editedDepartment && this.editedDepartment.id === department.id) {
            return this.editTemplate;
        } else {
            return this.readOnlyTemplate;
        }
    }
    
    loadTemplateEmployee(empployee: Employee) {
        return this.readOnlyTemplateEmployee;
    }
    

    saveDepartment() {
        if (this.isNewRecord) {
            this.serv.createDepartment(this.editedDepartment).subscribe(data => {
                this.statusMessage = 'Data added successfully',
                this.loadDepartments(this.pageNumber);
            },
                error => {this.statusMessage = error.message;}
            );
            this.isNewRecord = false;
            this.editedDepartment = null;
        } else {
            this.serv.updateDepartment(this.editedDepartment).subscribe(data => {
                this.statusMessage = 'Data update successfully',
                this.loadDepartments(this.pageNumber);
            }, 
               error => {this.statusMessage = error.message;}
            );
            this.editedDepartment = null;
        }
    }

    cancel() {
        if (this.isNewRecord) {
            this.departments.pop();
            this.isNewRecord = false;
        }
        this.editedDepartment = null;
    }
 
    deleteDepartment(department: Department) {
        const modal=this._modalService.open(NgbdModalConfirm);
        modal.result.then(result=> {
                this.serv.deleteDepartment(department.id).subscribe(data => {
                this.statusMessage = 'Данные успешно удалены',
                this.loadDepartments(this.pageNumber);
                this.isError=false;
            },
               error => {
                    this.statusMessage = error.error; 
                    this.isError=true;
                });
            },
            dismiss=>{
            }
        )
    }


    removeEmployee(employee:Employee)
    {
        const modal=this._modalService.open(NgbdModalConfirm);
        modal.result.then(result=> {
                var oldId=employee.departmentId;
                employee.departmentId=1;
                this.servEmployee.updateEmployee(employee).subscribe(data => {
                this.statusMessage = 'Data update successfully',
                this.openEmployees(oldId);
                this.isError=false;
            },
               error => {
                    this.statusMessage = error.error; 
                    this.isError=true;
                });
            },
            dismiss=>{
            }
        )
    }

    openEmployees(id:number) 
    {
        this.openDeprtmentId=id;
        this.employees=new Array<Employee>();
        this.loadEmployees(id);
    }           
}