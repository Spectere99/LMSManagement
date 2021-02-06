import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service';

export class PaRequestNote {
    Id: number;
    PaRequestId: number;
    NoteText: string;
    IsPublic: boolean;
    Created: string;
    CreatedBy: string;
    LastModified: string;
    LastModifiedBy: string;
    Archived: boolean;

}

@Injectable()
export class PaRequestNoteService {
    baseURL = environment.baseURL + '/api/';
    _ACTION = 'PaRequestNotes';

    constructor(private http: Http, private _authService: AuthenticationService) { }

    private getHeaders(queryHeaders) {
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

    getPaRequestNotes(id): Observable<any> {
        const queryHeaders = [{
            key: 'RequestId',
            value: id
        }];

        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(queryHeaders) })
            .pipe(map(res => res.json()));
        /* .map((res: Response) => {
            // console.log(res.json());
            return res.json();
        }); */
    }

    savePaRequestNote(userId, paRequestNote: PaRequestNote) {
        if (paRequestNote.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION, paRequestNote, { headers: this.getHeaders(undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            console.log('updating request record', paRequestNote);
            return this.http.put(this.baseURL + this._ACTION + '/' + paRequestNote.Id, paRequestNote,
                                { headers: this.getHeaders(undefined) })
            .pipe(map(res => res.json()));
        }
    }
}
