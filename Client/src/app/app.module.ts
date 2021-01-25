import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeeComponent } from './employee/employee.component';
import { PositionComponent } from './position/position.component';
import { DepartmentComponent } from './department/department.component';
import { NgbActiveModal, NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalConfirm } from './modal/modal.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    DepartmentComponent,
    EmployeeComponent,
    PositionComponent,
    NgbdModalConfirm,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
    { path: 'departments', component: DepartmentComponent },
    { path: 'employees', component: EmployeeComponent },
    { path: 'positions', component: PositionComponent },
], { relativeLinkResolution: 'legacy' })
  ],
  exports:[EmployeeComponent],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [NgbdModalConfirm],
})
export class AppModule { }
