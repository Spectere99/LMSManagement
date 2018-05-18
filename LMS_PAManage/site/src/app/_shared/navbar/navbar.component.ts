import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public isNavbarCollapsed = true;
  withShadingOptionsVisible: boolean;

  constructor() {
    this.withShadingOptionsVisible = false;
   }

  /* toggleMenu() {
    this.isCollapsed = !this.isCollapsed;
  } */

  toggleWiithShadingOptions() {
    this.withShadingOptionsVisible = !this.withShadingOptionsVisible;
  }
  ngOnInit() {
  }

}
