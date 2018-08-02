import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { AuthenticationService } from '../_services/authentication.service';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { query } from '@angular/animations';

export class FileUploadLog {
    Id: number;
    Uploaded: string;
    FileName: string;
    SourceIpAddress: string;
    RecordCount: number;
    SuccessCount: number;
    FailureCount: number;
    Created: string;
    CreatedBy: string;
    ModuleId: number;
    Archived: boolean;
}

@Injectable()
export class FileUploadService {

    baseURL = environment.baseURL + '/api/';
    _ACTION = 'FileUploadLogs';
    constructor(private http: Http, private _authService: AuthenticationService) { }

    private getHeaders(queryHeaders) {
        const headers = new Headers({ 'Accept': 'application/json' });
        const token = JSON.parse(this._authService.getUserToken());
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        console.log('User Token', token);
        headers.append('token', token.token);
        if (queryHeaders) {
            queryHeaders.forEach((qry) => {
                console.log('queryHeaders', qry);
                headers.append(qry.key, qry.value);
            });
        }
        // headers.append('showInactive'; this.showInactive.toString());
        return headers;
    }

    getFileUploads(userId, showArchived: boolean): Observable<any> {
        // Build customer odata Options
        const queryHeaders = [
            {
                key: 'showArchived',
                value: showArchived
            }];
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(queryHeaders) })
            .pipe(map(res => res.json()));
        /* .map((res: Response) => {
            // console.log(res.json());
            return res.json();
        }); */
    }

    getTodayFileUploads(userId, showArchived: boolean): Observable<any> {
        // Build customer odata Options
        const today = new Date();
        today.setHours(0, 0, 0, 0 );
        const queryHeaders = [
            {
                key: 'logStartDate',
                value: today.toLocaleString()
            },
            {
                key: 'showArchived',
                value: showArchived
            }];
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(queryHeaders) })
            .pipe(map(res => res.json()));
        /* .map((res: Response) => {
            // console.log(res.json());
            return res.json();
        }); */
    }

    saveFileUpload(userId, fileUpload: FileUploadLog): Observable<any> {
        // Build customer odata Options

        if (fileUpload.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION, fileUpload, { headers: this.getHeaders(undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            console.log('updating fileUpload record', fileUpload);
            return this.http.put(this.baseURL + this._ACTION + '/' + fileUpload.Id, fileUpload,
                                { headers: this.getHeaders(undefined) })
            .pipe(map(res => res.json()));
        }
    }
}
