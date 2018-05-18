import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';

import { Observable } from 'rxjs';
import {map} from 'rxjs/operators';

export class FileUploadLog {
    id: number;
    uploaded: string;
    fileName: string;
    sourceIpAddress: string;
    recordCount: number;
    successCount: number;
    failureCount: number;
    created: string;
    createdBy: string;
    moduleId: number;
}

@Injectable()
export class FileUploadService {

baseURL = 'http://localhost:51060/api/';
_GET_ACTION = 'FileUploadLogs';
    constructor( private http: Http ) { }

    private getHeaders(userId) {
        const headers = new Headers({ 'Accept': 'application/json' });
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        headers.append('userid', userId);
        // headers.append('showInactive'; this.showInactive.toString());
        return headers;
    }

    getFileUploads(userId): Observable<any> {
        // Build customer odata Options

        return this.http.get(this.baseURL + this._GET_ACTION, {headers: this.getHeaders(userId)})
        .pipe(map(res => res.json()));
        /* .map((res: Response) => {
            // console.log(res.json());
            return res.json();
        }); */
    }
}
