import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';
import { Globals } from '../globals';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {

  constructor(private globals: Globals, private _authService: AuthenticationService, private _router: Router) {
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean>|Promise<boolean>|boolean {
    return this.performCheck(childRoute, state);
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    // console.log('canActiviate', next);
    return this.performCheck(next, state);
  }

  private performCheck(next: ActivatedRouteSnapshot,
                       state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {

    // console.log('performCheck', this._authService.isAuthenticated());
    if (this._authService.isAuthenticated()) {
        const userToken = this._authService.getDecodedUserToken();
        // console.log('logged in User Token', userToken);
        this.globals.isAdmin = userToken.isAdmin;
        this.globals.user.userId = userToken.userLoginId;
        this.globals.user.userName = userToken.unique_name;
        this.globals.user.userFullName = userToken.displayName;
        this.globals.user.userRole = userToken.role;
        this.globals.isAuthenticated = true;
        return true;
      }

    this._authService.setRedirectUrl(state.url);
    this._authService.message = 'Please login to continue';
    this._authService.clear();

    this.globals.isAdmin = false;
    this.globals.user.userId = 0;
    this.globals.user.userName = '';
    this.globals.user.userFullName = '';
    this.globals.user.userRole = '';
    this.globals.isAuthenticated = false;

    this._router.navigate(['/Login']);
    return false;
                       }

}
