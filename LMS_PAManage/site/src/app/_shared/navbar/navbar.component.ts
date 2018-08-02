import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../_services/authentication.service';
import { Globals } from '../../globals';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public isNavbarCollapsed = true;
  environmentName = environment.environmentName;
  version = environment.version;
  withShadingOptionsVisible: boolean;

  constructor(public globals: Globals, public _authService: AuthenticationService, public _router: Router) {
    this.withShadingOptionsVisible = false;
   }

  /* toggleMenu() {
    this.isCollapsed = !this.isCollapsed;
  } */

  toggleWiithShadingOptions() {
    this.withShadingOptionsVisible = !this.withShadingOptionsVisible;
  }

  logout(): void {
    console.log('logging out');
    this._authService.logout();
    this.globals.isAuthenticated = false;
    this._router.navigate(['/Login']);
  }

  ngOnInit() {
  }

}
