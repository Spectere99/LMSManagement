import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import {
    DxSelectBoxModule,
    DxDataGridModule,
    DxDataGridComponent
} from 'devextreme-angular';
// import 'devextreme/integration/jquery';
// import { PrescriptionAuthorization, PAAuthService } from '../../_services/pa.service';
import { Globals } from '../../globals';
import { AuthenticationService } from '../../_services/authentication.service';
import { PaRequest, PaRequestService } from '../../_services/pa-request.service';
import { FileUploadLog, FileUploadService } from '../../_services/file-upload.service';
import { LookupItem, LookupType, LookupService } from '../../_services/lookup.service';
import { InsuranceCompany, InsuranceCompanyService } from '../../_services/insurance-company.service';
import { User, UserService } from '../../_services/user.service';
import { CompileNgModuleMetadata } from '@angular/compiler';

@Component({
    selector: 'app-pa-list',
    templateUrl: './pa-list.component.html',
    styleUrls: ['./pa-list.component.sass'],
    providers: [LookupService,
        InsuranceCompanyService,
        PaRequestService,
        FileUploadService,
        UserService]
})
export class PaListComponent implements OnInit {
    @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
    securityMessage = '';
    paRequests: PaRequest[] = [];
    fileUploadLogs: FileUploadLog[] = [];
    insuranceLookup: InsuranceCompany[] = [];
    statusLookup: LookupItem[] = [];
    billingStatusLookup: LookupItem[] = [];
    selectedItemKeys: any[] = [];
    users: User[] = [];
    selectedUser: any;
    currentBatch: any;
    currentBatchId: any;

    // Row Color Constants
    _PRIORITY_ROW_COLOR = 'tan';
    _COMPLETED_ROW_TEXT = 'lightgrey';
    _APPROVED_ROW_COLOR = 'green';
    _ON_HOLD_ROW_COLOR = 'lightgrey';
    _DEINED_ROW_COLOR = 'red';
    _APPEAL_ROW_COLOR = 'orange';
    _SUBMITTED_ROW_COLOR = 'lightblue';
    _ASSIGNED_ROW_COLOR = 'pink';
    _INCOMPLETE_ROW_COLOR = 'plum';

    constructor(public globals: Globals, private _router: Router, public lookupService: LookupService,
        public paRequestService: PaRequestService, public fileUploadService: FileUploadService,
        public insuranceCompanyService: InsuranceCompanyService, public userService: UserService,
        private el: ElementRef) {

        if (!globals.isAdmin) {
            this.pullAssignedRequests();
        }

        // console.log('const: currentBatch', this.currentBatch);
        fileUploadService.getFileUploads(globals.user.userName, false).subscribe(res => {
            this.fileUploadLogs = res;
            // console.log('const: fileUploadLogs', this.fileUploadLogs);
            // console.log('const: session Batch number', sessionStorage.getItem('currentBatch'));
            // console.log('const: batch to Load', this.fileUploadLogs.filter
            //    (x => x.Id === parseInt(sessionStorage.getItem('currentBatch'), 0)));
            this.currentBatch = this.fileUploadLogs.filter(x => x.Id === parseInt(sessionStorage.getItem('currentBatch'), 0));

            if (this.currentBatch !== undefined) {
                // console.log('pulling batch Requests', this.currentBatch);
                this.currentBatchId = parseInt(sessionStorage.getItem('currentBatch'), 0);
                this.pullBatchRequests(parseInt(sessionStorage.getItem('currentBatch'), 0));
            }

        });
        lookupService.getLookupTypes(globals.user.userName).subscribe(res => {
            const requestLookupType = res.filter(p => p.Id === 3); // Pre seeded database for 'Pa_RequestStatus'
            if (requestLookupType.length > 0) {
                this.statusLookup = requestLookupType[0].Lookups;
            }
            const billingLookupType = res.filter(p => p.Id === 4); // Pre seeded database for 'BillingStatus'
            if (billingLookupType.length > 0) {
                this.billingStatusLookup = billingLookupType[0].Lookups;
                // console.log(this.billingStatusLookup);
            }
        });
        insuranceCompanyService.getInsuranceCompanies(globals.user.userName).subscribe(res => {
            this.insuranceLookup = res;
        });
        userService.getUsers(globals.user.userName, false).subscribe(res => {
            this.users = res;
        });
    }
    onContentReady(e) {
        e.component.columnOption('command:edit', {
            visibleIndex: -1,
            width: 80
        });
    }

    selectionChanged(data: any) {
        this.selectedItemKeys = data.selectedRowKeys;
    }

    setHeight() {
        return window.innerHeight - (window.innerHeight * .30);
    }

    pullAssignedRequests() {
        this.paRequestService.getUsersBatchPaRequests(this.globals.user.userName, this.globals.user.userName)
            .subscribe(res => {
                this.paRequests = res;
                // console.log(this.paRequests);
            }, (error) => {
                if (error.status === 401) {
                    this.securityMessage = error._body;
                    console.error('Pa-list:pullBatchRequests (auth error)', error);
                    setTimeout(() => {
                        console.log('redirecting');
                        this.globals.isAuthenticated = false;
                        this._router.navigate(['/Login']);
                    }, 10000);
                }
            });
    }
    onBatchRequestSelected(e) {
        sessionStorage.setItem('currentBatch', e.selectedItem.Id);
        // console.log('onBatchRequestSelected', e.selectedItem.Id);
        this.currentBatchId = parseInt(sessionStorage.getItem('currentBatch'), 0);
        this.pullBatchRequests(e.selectedItem.Id);
    }
    pullBatchRequests(batchId) {
        if (!this.globals.isAdmin) {
            // console.log('pullBatchRequests', this.globals.isAdmin);
            this.paRequestService.getUsersBatchPaRequests(this.globals.user.userName, this.globals.user.userName)
                .subscribe(res => {
                    this.paRequests = res;
                    // console.log(this.paRequests);
                }, (error) => {
                    if (error.status === 401) {
                        this.securityMessage = error._body;
                        console.error('Pa-list:pullBatchRequests (auth error)', error);
                        setTimeout(() => {
                            console.log('redirecting');
                            this.globals.isAuthenticated = false;
                            this._router.navigate(['/Login']);
                        }, 10000);
                    }
                });
        } else {
            // console.log('pullBatchRequests', this.globals.isAdmin, batchId);
            this.paRequestService.getBatchPaRequests(this.globals.user.userName, batchId)
                .subscribe(res => {
                    this.paRequests = res;
                    // console.log(this.paRequests);
                }, (error) => {
                    if (error.status === 401) {
                        this.securityMessage = error._body;
                        console.error('Pa-list:pullBatchRequests (auth error)', error);
                        setTimeout(() => {
                            console.log('redirecting');
                            this.globals.isAuthenticated = false;
                            this._router.navigate(['/Login']);
                        }, 10000);
                    }
                });
        }
    }
    batchAssign(e) {
        // console.log('Selected Item Keys:', this.selectedItemKeys);
        // console.log('Selected User:', this.selectedUser);
        // console.log('Users:', this.users);
        const selectedUserObject = this.getUserByUserName(this.selectedUser);
        this.selectedItemKeys.forEach(item => {
            // console.log('Item to Batch Set', item);
            item.AssignedTo = selectedUserObject === undefined ? item.AssignedTo : selectedUserObject.Id ;
            item.Assigned = new Date().toISOString();
            item.Status = 3;  // System Set for 'Assigned' status
            this.paRequestService.savePaRequest(this.globals.user.userName, item)
                .subscribe(res => {
                }, (error) => {
                    if (error.status === 401) {
                        this.securityMessage = error._body;
                        console.error('Pa-list:batchAssign (auth error)', error);
                        setTimeout(() => {
                            console.log('redirecting');
                            this.globals.isAuthenticated = false;
                            this._router.navigate(['/Login']);
                        }, 10000);
                    }
                });
        });
        this.dataGrid.instance.clearSelection();
        // console.log(this.selectedItemKeys);
    }

    getUserByUserName(userName: string) {
        const user = this.users.filter(x => x.UserName === userName)[0];
        if (user != null) {
            return user;
        } else {
            return null;
        }
    }
    onRowPrepared(e) {
        // console.log(e);
        if (e.rowType === 'data') {
            if (e.data.Completed) {
                // console.log(e);
                e.rowElement.style.color = this._COMPLETED_ROW_TEXT;
            }
            if (e.data.Priority === true) {
                // console.log('Priority', e.data.Priority);
                e.rowElement.bgColor = this._PRIORITY_ROW_COLOR;
            }
            switch (e.data.Status) {
                // ADD OTHER STATUS FOR ROW COLORS
                case 2: {  // Pending
                   // e.rowElement.bgColor = this._APPROVED_ROW_COLOR;
                    break;
                }
                case 3: { // Assigned
                    e.rowElement.bgColor = this._ASSIGNED_ROW_COLOR;
                    break;
                }
                case 4: { // Submitted
                    e.rowElement.bgColor = this._SUBMITTED_ROW_COLOR;
                    break;
                }
                case 5: { // Incomplete
                    e.rowElement.bgColor = this._INCOMPLETE_ROW_COLOR;
                    break;
                }
                case 6: { // On Hold
                    e.rowElement.bgColor = this._ON_HOLD_ROW_COLOR;
                    break;
                }
                case 7: { // Approved
                    e.rowElement.bgColor = this._APPROVED_ROW_COLOR;
                    break;
                }
                case 8: { // Denied
                    e.rowElement.bgColor = this._DEINED_ROW_COLOR;
                    break;
                }
                case 9: { // Appeal
                    e.rowElement.bgColor = this._APPEAL_ROW_COLOR;
                    break;
                }
            }

        }
    }
    onCellPrepared(e) {
        if (e.rowType === 'data' && e.column.command === 'edit') {
            const isEditing = e.row.isEditing,
                cellElement = e.cellElement;

            if (isEditing) {
                const saveLink = cellElement.querySelector('.dx-link-save'),
                    cancelLink = cellElement.querySelector('.dx-link-cancel');

                saveLink.classList.add('dx-icon-save');
                cancelLink.classList.add('dx-icon-revert');

                saveLink.textContent = '';
                cancelLink.textContent = '';
            } else {
                const editLink = cellElement.querySelector('.dx-link-edit');
                // deleteLink = cellElement.querySelector('.dx-link-delete');

                editLink.classList.add('dx-icon-edit');
                // deleteLink.classList.add('dx-icon-trash');

                editLink.textContent = '';
                // deleteLink.textContent = '';
            }
        }
    }
    onEditorPreparing(e: any) {
        if (e.parentType === 'dataRow' && e.dataField === 'Note') {
            e.editorName = 'dxTextArea';
        }
    }

    initRecord(e) {
        // console.log('InitRecord', this.insuranceLookup);
        const addRec: PaRequest = {
            Id: 0,
            FileUploadLogId: 1,
            Approval: null,
            ApprovalDocumentUrl: null,
            Archived: false,
            Assigned: null,
            AssignedTo: null,
            Created: new Date().toISOString(),
            CreatedBy: this.globals.user.userName,
            Denial: null,
            DoctorName: null,
            DrugName: null,
            InsuranceCompany_Id: null,
            LastModified: new Date().toISOString(),
            LastModifiedBy: this.globals.user.userName,
            Note: null,
            PatientName: null,
            Status: '',
            Submitted: new Date().toISOString(),
            Completed: false,
            CompletedTimeStamp: null,
            // d.newData.CompletedTimeStamp === undefined ? d.oldData.CompletedTimeStamp : d.newData.CompletedTimeStamp,
            Priority: false,
            BillingStatus: 0,
            RequestReassign: false,
            AutomobileRelated: false,
            NonMeds: false
        };
        e.data = addRec;
    }

    addRecord(d) {
        // console.log('Adding Record', d);
        let completedDate = null;
        // See if the update contians a completed record change.
        if (d.data.Completed && d.data.Completed === true) {
            completedDate = new Date().toISOString();
        }
        const newRec: PaRequest = {
            Id: 0,
            FileUploadLogId: d.data.FileUploadLogId === null ? 1 : d.data.FileUploadLogId,
            Approval: d.data.approval === undefined ? null : d.data.approval,
            ApprovalDocumentUrl: d.data.approvalDocumentUrl === undefined
                ? null
                : d.data.ApprovalDocumentUrl,
            Archived: d.data.Archived === undefined ? false : d.data.Archived,
            Assigned: d.data.Assigned === undefined ? null : new Date().toISOString(),
            AssignedTo: d.data.AssignedTo === undefined ? null : d.data.AssignedTo,
            Created: d.data.Created === undefined ? new Date().toISOString() : d.data.Created,
            CreatedBy: d.data.CreatedBy === undefined ? this.globals.user.userName : d.data.CreatedBy,
            Denial: d.data.Denial === undefined ? null : d.data.Denial,
            DoctorName: d.data.DoctorName === undefined ? null : d.data.DoctorName,
            DrugName: d.data.DrugName === undefined ? null : d.data.DrugName,
            InsuranceCompany_Id: d.data.InsuranceCompany_Id === null
                ? 0
                : d.data.InsuranceCompany_Id,
            LastModified: new Date().toISOString(),
            LastModifiedBy: this.globals.user.userName,
            Note: d.data.Note === undefined ? null : d.data.Note,
            PatientName: d.data.PatientName === undefined ? null : d.data.PatientName,
            Status: d.data.Status === undefined ? '' : d.data.Status,
            Submitted: d.data.Submitted === undefined ? new Date().toISOString() : d.data.Submitted,
            Completed: d.data.Completed === undefined ? false : d.data.Completed,
            CompletedTimeStamp: completedDate,
            // d.newData.CompletedTimeStamp === undefined ? d.oldData.CompletedTimeStamp : d.newData.CompletedTimeStamp,
            Priority: d.data.Priority === undefined ? false : d.data.Priority,
            BillingStatus: d.data.BillingStatus === undefined ? 0 : d.data.BillingStatus,
            RequestReassign: d.data.RequestReassign === undefined ? false : d.data.RequestReassign,
            AutomobileRelated: d.data.AutomobileRelated === undefined ? false : d.data.AutomobileRelated,
            NonMeds: d.data.NonMeds === undefined ? false : d.data.NonMeds,
        };

        this.paRequestService.savePaRequest(this.globals.user.userName, newRec)
            .subscribe(res => {
            }, (error) => {
                if (error.status === 401) {
                    this.securityMessage = error._body;
                    console.error('Pa-list:updateRecord (auth error)', error);
                    setTimeout(() => {
                        console.log('redirecting');
                        this.globals.isAuthenticated = false;
                        this._router.navigate(['/Login']);
                    }, 10000);
                }
            });
    }

    updateRecord(d) {
        // console.log('Saving Record', d);
        let completedDate = null;
        // See if the update contians a completed record change.
        if (d.newData.Completed && d.newData.Completed === true) {
            completedDate = new Date().toISOString();
        }
        const updRec: PaRequest = {
            Id: d.key.Id,
            FileUploadLogId: d.oldData.FileUploadLogId,
            Approval: d.newData.approval === undefined ? d.oldData.approval : d.newData.approval,
            ApprovalDocumentUrl: d.newData.approvalDocumentUrl === undefined
                ? d.oldData.ApprovalDocumentUrl
                : d.newData.ApprovalDocumentUrl,
            Archived: d.newData.Archived === undefined ? d.oldData.Archived : d.newData.Archived,
            Assigned: d.newData.AssignedTo === d.oldData.AssignedTo ? d.oldData.Assigned : new Date().toISOString(),
            AssignedTo: d.newData.AssignedTo === undefined ? d.oldData.AssignedTo : d.newData.AssignedTo,
            Created: d.newData.Created === undefined ? d.oldData.Created : d.newData.Created,
            CreatedBy: d.newData.CreatedBy === undefined ? d.oldData.CreatedBy : d.newData.CreatedBy,
            Denial: d.newData.Denial === undefined ? d.oldData.Denial : d.newData.Denial,
            DoctorName: d.newData.DoctorName === undefined ? d.oldData.DoctorName : d.newData.DoctorName,
            DrugName: d.newData.DrugName === undefined ? d.oldData.DrugName : d.newData.DrugName,
            InsuranceCompany_Id: d.newData.InsuranceCompany_Id === undefined
                ? d.oldData.InsuranceCompany_Id
                : d.newData.InsuranceCompany_Id,
            LastModified: new Date().toISOString(),
            LastModifiedBy: this.globals.user.userName,
            Note: d.newData.Note === undefined ? d.oldData.Note : d.newData.Note,
            PatientName: d.newData.PatientName === undefined ? d.oldData.PatientName : d.newData.PatientName,
            Status: d.newData.Status === undefined ? d.oldData.Status : d.newData.Status,
            Submitted: d.newData.Submitted === undefined ? d.oldData.Submitted : d.newData.Submitted,
            Completed: d.newData.Completed === undefined ? d.oldData.Completed : d.newData.Completed,
            CompletedTimeStamp: completedDate,
            // d.newData.CompletedTimeStamp === undefined ? d.oldData.CompletedTimeStamp : d.newData.CompletedTimeStamp,
            Priority: d.newData.Priority === undefined ? d.oldData.Priority : d.newData.Priority,
            BillingStatus: d.newData.BillingStatus === undefined ? d.oldData.BillingStatus : d.newData.BillingStatus,
            RequestReassign: d.newData.RequestReassign === undefined ? d.oldData.RequestReassign : d.newData.RequestReassign,
            AutomobileRelated: d.newData.AutomobileRelated === undefined ? d.oldData.AutomobileRelated : d.newData.AutomobileRelated,
            NonMeds: d.newData.NonMeds === undefined ? d.oldData.NonMeds : d.newData.NonMeds,
        };

        // console.log('updRec', updRec);

        this.paRequestService.savePaRequest(this.globals.user.userName, updRec)
            .subscribe(res => {
            }, (error) => {
                if (error.status === 401) {
                    this.securityMessage = error._body;
                    console.error('Pa-list:updateRecord (auth error)', error);
                    setTimeout(() => {
                        console.log('redirecting');
                        this.globals.isAuthenticated = false;
                        this._router.navigate(['/Login']);
                    }, 10000);
                }
            });
    }

    ngOnInit() {
        this.currentBatchId = parseInt(sessionStorage.getItem('currentBatch'), 0);
    }

}
