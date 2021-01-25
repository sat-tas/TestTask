import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent{
  isExpanded = false;
  constructor(private router: Router) {
  };

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

