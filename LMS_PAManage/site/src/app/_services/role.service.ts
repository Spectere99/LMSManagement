import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service';

export class Role {
    Id: number;
    Name: string;
    Created: string;
    CreatedBy: string;
    LastModified: string;
    LastModifiedBy: string;
    Archived: boolean;
}

@Injectable()
export class RoleService {
    baseURL = environment.baseURL + '/api/';
    _ACTION = 'Roles';

    constructor(private http: Http, private _authService: AuthenticationService) { }

    private getHeaders(userId, queryHeaders) {
        const headers = new Headers({ 'Accept': 'application/json' });
        const token = JSON.parse(this._authService.getUserToken());
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        // console.log('User Token', token);
        // headers.append('token', token.token);
        if (queryHeaders) {
            queryHeaders.forEach((qry) => {
                // console.log('queryHeaders', qry);
                headers.append(qry.key, qry.value);
            });
        }
        // headers.append('showInactive'; this.showInactive.toString());
        return headers;
    }

    getRoles(userId): Observable<any> {
        // Build customer odata Options
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
    }

    saveRole(userId, role: Role) {
        if (role.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION, role, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            return this.http.put(this.baseURL + this._ACTION + '/' + role.Id, role,
                                { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        }
    }
}
