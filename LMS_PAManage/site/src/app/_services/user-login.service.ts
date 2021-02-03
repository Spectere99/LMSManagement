import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service';

export class UserLoginCreation {
    Id: number;
    Login: string;
    Password: string;
    IsAdmin: boolean;
}


@Injectable()
export class UserLoginService {
    baseURL = environment.baseURL + '/api/';
    _ACTION = 'UserLoginCreation';

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

    getUserLogins(userId): Observable<any> {
        // Build customer odata Options
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
    }

    saveUserLoginCreation(userId, userLoginCreation: UserLoginCreation) {
        if (userLoginCreation.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION, userLoginCreation, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            return this.http.put(this.baseURL + this._ACTION + '/' + userLoginCreation.Id, userLoginCreation,
                                { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        }
    }
}
