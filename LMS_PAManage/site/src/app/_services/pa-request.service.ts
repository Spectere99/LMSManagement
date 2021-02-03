import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service';

export class PaRequest {
    Id: number;
    FileUploadLogId: number;
    PatientName: string;
    DoctorName: string;
    DrugName: string;
    InsuranceCompany_Id: string;
    Status: string;
    Submitted: string;
    Approval: string;
    Denial: string;
    ApprovalDocumentUrl: string;
    Note: string;
    Assigned: string;
    AssignedTo: string;
    Created: string;
    CreatedBy: string;
    LastModified: string;
    LastModifiedBy: string;
    Archived: boolean;
    Completed: boolean;
    CompletedTimeStamp: string;
    Priority: boolean;
    BillingStatus: number;
    RequestReassign: boolean;
    AutomobileRelated: boolean;
    NonMeds: boolean;
}

export class BatchStatistic {
    Display: string;
    Count: number;
}

@Injectable()
export class PaRequestService {
    baseURL = environment.baseURL + '/api/';
    _ACTION = 'PaRequests';

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

    getPaRequests(userId): Observable<any> {
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(undefined) })
            .pipe(map(res => res.json()));
        /* .map((res: Response) => {
            // console.log(res.json());
            return res.json();
        }); */
    }

    getBatchPaRequests(userId, batchId: number): Observable<any> {
        // Build customer odata Options
        const queryHeaders = [{
            key: 'Id',
            value: batchId
        }];
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(queryHeaders) })
            .pipe(map(res => res.json()));
        /* .map((res: Response) => {
            // console.log(res.json());
            return res.json();
        }); */
    }

    getUsersBatchPaRequests(userId, assignedTo: string): Observable<any> {
        // console.log('getUsersBatchPaRequests');
        const queryHeaders = [
        {
            key: 'AssignedTo',
            value: assignedTo
        }];
        return this.http.get(this.baseURL + this._ACTION, { headers: this.getHeaders(queryHeaders) })
            .pipe(map(res => res.json()));
        /* .map((res: Response) => {
            // console.log(res.json());
            return res.json();
        }); */
    }

    savePaRequest(userId, paRequest: PaRequest) {
        if (paRequest.Id === 0) {  // Insert
            return this.http.post(this.baseURL + this._ACTION, paRequest, { headers: this.getHeaders(undefined) })
            .pipe(map(res => res.json()));
        } else {  // Update
            // console.log('updating request record', paRequest);
            return this.http.put(this.baseURL + this._ACTION + '/' + paRequest.Id, paRequest,
                                { headers: this.getHeaders(undefined) })
            .pipe(map(res => res.json()));
        }
    }
}
