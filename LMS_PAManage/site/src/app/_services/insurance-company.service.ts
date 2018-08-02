import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export class InsuranceCompany {
    Id: number;
    CompanyName: string;
    CompanyCode: string;
    Created: string;
    CreatedBy: string;
    LastModified: string;
    LastModifiedBy: string;
    Archived: boolean;
}

@Injectable()
export class InsuranceCompanyService {
    baseURL = environment.baseURL + '/api/';
    _ACTION = 'InsuranceCompanies';

    constructor(private http: Http) { }

    private getHeaders(userId, queryHeaders) {
        const headers = new Headers({ 'Accept': 'application/json' });
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        headers.append('userid', userId);
        if (queryHeaders) {
            queryHeaders.forEach((qry) => {
                console.log('queryHeaders', qry);
                headers.append(qry.key, qry.value);
            });
        }
        // headers.append('showInactive'; this.showInactive.toString());
        return headers;
    }

    getInsuranceCompanies(userId): Observable<any> {
        // Build customer odata Options
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
    }

    saveInsuranceCompany(userId, insuranceCompany: InsuranceCompany) {
        if (insuranceCompany.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION, insuranceCompany, { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            return this.http.put(this.baseURL + this._ACTION + '/' + insuranceCompany.Id, insuranceCompany,
                                { headers: this.getHeaders(userId, undefined) })
            .pipe(map(res => res.json()));
        }
    }
}
