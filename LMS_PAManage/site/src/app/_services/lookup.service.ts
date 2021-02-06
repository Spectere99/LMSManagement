import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export class LookupItem {
    Id: number;
    LookupTypeId: number;
    LookupValue: string;
    Created: string;
    CreatedBy: string;
    LastModified: string;
    LastModifiedBy: string;
    Archived: boolean;
}

export class LookupType {
    Id: number;
    Type: string;
    Archived: boolean;
    Lookups: LookupItem[];
}

@Injectable()
export class LookupService {
    baseURL = environment.baseURL + '/api/';
    _ACTION = 'Lookups';
    _ACTION_TYPES = 'LookupTypes';

    constructor(private http: Http) { }

    private getHeaders(userId, queryHeaders) {
        const headers = new Headers({ 'Accept': 'application/json' });
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        headers.append('userid', userId);
        if (queryHeaders) {
            queryHeaders.forEach((qry) => {
                // console.log('queryHeaders', qry);
                headers.append(qry.key, qry.value);
            });
        }
        // headers.append('showInactive'; this.showInactive.toString());
        return headers;
    }

    getLookupTypes(userId): Observable<any> {
        // Build customer odata Options
        return this.http.get(this.baseURL + this._ACTION_TYPES, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
    }

    getLookupsByType(userId, lookupTypeId: number) {
        const queryHeaders = [
            {
                key: 'lookupTypeId',
                value: lookupTypeId
            }];
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(userId, queryHeaders) })
            .pipe(map(res => res.json()));
    }

    saveLookupItem(userId, lookup: LookupItem) {
        if (lookup.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION, lookup, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            return this.http.put(this.baseURL + this._ACTION + '/' + lookup.Id, lookup,
                                { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        }
    }

    saveLookupType(userId, lookupType: LookupType) {
        if (lookupType.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION_TYPES, lookupType, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            return this.http.put(this.baseURL + this._ACTION_TYPES + '/' + lookupType.Id, lookupType,
                                { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        }
    }
}
