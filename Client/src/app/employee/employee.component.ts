import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {Observable} from 'rxjs';
import { Employee } from '../entities/emploee';
import { EmployeeService } from '../services/emploee.service';
import { Department } from '../entities/department';
import { DepartmentService } from '../services/department.service';
import { Position } from '../entities/position';
import { PositionService } from '../services/position.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalConfirm } from '../modal/modal.component';

@Component({ 
    selector: 'app-home', 
    templateUrl: './employee.component.html',
    styleUrls:['./employee.component.css'],
    providers: [EmployeeService]
}) 
export class EmployeeComponent implements OnInit {
    @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>;
    @ViewChild('editTemplate', {static: false}) editTemplate: TemplateRef<any>;
     
    departments: Array<Department>;
    positions: Array<Position>;
    editedEmployee: Employee;
    employees: Array<Employee>;
    isNewRecord: boolean;
    statusMessage: string="Output";
    isError:boolean=false;
  
    constructor(private serv: EmployeeService,private departmentServ: DepartmentService,private positionServ: PositionService,private _modalService: NgbModal) 
    {
        this.departments = new Array<Department>();
        this.employees = new Array<Employee>();
        this.positions = new Array<Position>();
    }
       
    ngOnInit() {
        this.loadEmployees();
        this.loadDepartments();
        this.loadPositions();
    }
       
    private loadDepartments() {
        this.departmentServ.getDepartments().subscribe((data: Department[]) => {
                this.departments = data; 
            });
    }

    private loadPositions() {
        this.positionServ.getPositions().subscribe((data: Position[]) => {
                this.positions = data; 
            });
    }


    private loadEmployees() {
        this.serv.getEmployees().subscribe((data: Employee[]) => {
                this.employees = data; 
            });
    }

    selectDepartmentChangeHandler(event:any)
    {
        this.editedEmployee.departmentId=event.target.value; 
    }

    selectPositionChangeHandler(event:any)
    {
        this.editedEmployee.positionId=event.target.value; 
    }
    addEmployee() {
        this.editedEmployee = new Employee(0,"","","",1,"",1,"",null,null,null);
        this.employees.push(this.editedEmployee);
        this.isNewRecord = true;
    }
    
    editEmployee(employee: Employee) {
        this.editedEmployee = new Employee(employee.id,employee.name,employee.surname,employee.patronymic,employee.departmentId,employee.departmentName,employee.positionId,employee.positionName, employee.dateAdded,employee.dateChangeInfo,employee.dateOfHiring);
    }

    loadTemplate(employee: Employee) {
        if (this.editedEmployee && this.editedEmployee.id === employee.id) {
            return this.editTemplate;
        } else {
            return this.readOnlyTemplate;
        }
    }
    
    saveEmployee() {
        if (this.isNewRecord) {
            this.serv.createEmployee(this.editedEmployee).subscribe(data => {
                this.statusMessage = 'Data added successfully',
                this.loadEmployees();
            });
            this.isNewRecord = false;
            this.editedEmployee = null;
        } else {
            this.serv.updateEmployee(this.editedEmployee).subscribe(data => {
                this.statusMessage = 'Data update successfully',
                this.loadEmployees();
            });
            this.editedEmployee = null;
        }
    }

    cancel() {
        if (this.isNewRecord) {
            this.employees.pop();
            this.isNewRecord = false;
        }
        this.editedEmployee = null;
    }
 
    deleteEmployee(employee: Employee) {
        const modal=this._modalService.open(NgbdModalConfirm);
        modal.result.then(result=> {
                this.serv.deleteEmployee(employee.id).subscribe(data => {
                this.statusMessage = 'Data deleted successfully',
                this.loadEmployees();
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
}