import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import 'rxjs/operators';

import * as jwt_decode from 'jwt-decode';

@Injectable()
export class AuthenticationService {
    public baseURL = environment.baseURL + '/api/Security';       // environment.authEndpoint;
    private redirectUrl: string;
    public _isLoggedIn = false;
    public token;
    public message: string;

    constructor(private http: Http) {
        // set token if saved in local storage
        // this.isLoggedIn();
    }

    private getHeaders(userId, password) {
        const headers = new Headers({ 'Accept': 'application/json' });
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        headers.append('userid', userId);
        headers.append('password', password);
        // headers.append('showInactive'; this.showInactive.toString());
        return headers;
    }

    login(username: string, password: string): Observable<string> {
        return this.http.post(this.baseURL, null, { headers: this.getHeaders(username, password)})
        .pipe(map(response => {
            // console.log('Return from login', response);
            const token = response.json();
            // console.log(token);
            if (token) {
                this.token = token;
                try {
                    const decodedToken = jwt_decode(token);
                    // console.log(decodedToken);
                    sessionStorage.setItem('currentUser', JSON.stringify({ username: username, token: token } ));
                    JSON.stringify(token);
                } catch (Error) {
                    console.log(Error);
                    return '';
                }
            } else {
                return '';
            }
        },
        error => {
            console.log('Error returned', error);
            return '';
        }));
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        this.clear();
    }

    isAuthenticated(): boolean {
        const currentUser = JSON.parse(sessionStorage.getItem('currentUser'));
        // console.log('isAuthenticated - currentUser', currentUser);
         this._isLoggedIn = false;
         if (currentUser) {
            this.token = currentUser.profile;
            this._isLoggedIn = true;
         }
         return this._isLoggedIn;
    }

    setRedirectUrl(url: string): void {
        this.redirectUrl = url;
    }
    getRedirectUrl(): string {
        return this.redirectUrl;
    }

    public getUserToken(): string {
        return sessionStorage.getItem('currentUser');
    }

    public getDecodedUserToken(): any {
        const token = sessionStorage.getItem('currentUser');
        // console.log('getDecodedUserToken', token);
        const decodedToken = jwt_decode(token);
        // console.log(decodedToken);

        return decodedToken;
    }

    clear(): void {
        // console.log('Clearing Local Storage');
        sessionStorage.removeItem('currentUser');
    }
}
