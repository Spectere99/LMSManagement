import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service';
import { Role } from '../_services/role.service';

export class User {
    Id: number;
    UserName: string;
    FirstName: string;
    LastName: string;
    Email: string;
    PhoneNumber: string;
    Created: string;
    CreatedBy: string;
    LastModified: string;
    LastModifiedBy: string;
    Archived: boolean;
    RoleId: number;
    Role: Role;
    UserLoginId: number;
    UserLogin: UserLogin;
}

export class UserLogin {
    Id: number;
    Created: string;
    CreatedBy: string;
    AccessFailedCount: number;
    IsAdmin: boolean;
    LastModified: string;
    LastModifiedBy: string;
    LockoutEnabled: boolean;
    LockoutEnd: string;
    Login: string;
    PasswordHash: string;
    RefreshId: number;
}

@Injectable()
export class UserService {
    baseURL = environment.baseURL + '/api/';
    _ACTION = 'Users';

    constructor(private http: Http, private _authService: AuthenticationService) { }

    private getHeaders(userId, queryHeaders) {
        const headers = new Headers({ 'Accept': 'application/json' });
        const token = JSON.parse(this._authService.getUserToken());
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        // console.log('User Token', token);
        headers.append('token', token.token);
        if (queryHeaders) {
            queryHeaders.forEach((qry) => {
                // console.log('queryHeaders', qry);
                headers.append(qry.key, qry.value);
            });
        }
        // headers.append('showInactive'; this.showInactive.toString());
        return headers;
    }

    getUsers(userId, showArchived: boolean): Observable<any> {
        // Build customer odata Options
        const queryHeaders = [
            {
                key: 'showArchived',
                value: showArchived
            }];
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(userId, queryHeaders) })
            .pipe(map(res => res.json()));
    }

    saveUser(userId, user: User) {
        if (user.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION, user, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            return this.http.put(this.baseURL + this._ACTION + '/' + user.Id, user,
                                { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        }
    }
}
