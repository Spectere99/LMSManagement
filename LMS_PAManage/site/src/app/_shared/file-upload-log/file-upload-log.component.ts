import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { FileUploadLog, FileUploadService } from '../../_services/file-upload.service';
import { PaRequest, BatchStatistic, PaRequestService } from '../../_services/pa-request.service';
import { LookupItem, LookupType, LookupService } from '../../_services/lookup.service';
import { Globals } from '../../globals';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-file-upload-log',
  templateUrl: './file-upload-log.component.html',
  styleUrls: ['./file-upload-log.component.sass'],
  providers: [FileUploadService,
    PaRequestService,
    LookupService]
})

export class FileUploadLogComponent implements OnInit {
  @Input() manageMode = false;
  uploadedFiles;
  showPopup = false;
  securityMessage = '';
  batchDetailTitle = '';
  currentBatch;
  statusLookup;
  billingStatusLookup;
  paRequests;
  batchStatistics: BatchStatistic[];
  billingStatistics: BatchStatistic[];
  // currentBatchStats;

  constructor(private globals: Globals, public lookupService: LookupService,
    private uploadService: FileUploadService, private paRequestService: PaRequestService,
    private _router: Router) {
    //  uploadService.getFileUploads('rwflowers').subscribe((res: Response) => this.uploadedFiles = res);
    lookupService.getLookupTypes(globals.user.userName).subscribe(res => {
      const requestLookupType = res.filter(p => p.Id === 3); // Pre seeded database for 'Pa_RequestStatus'
      if (requestLookupType.length > 0) {
        this.statusLookup = requestLookupType[0].Lookups;
        console.log('statusLookup', this.statusLookup);
      }
      const billingLookupType = res.filter(p => p.Id === 4); // Pre seeded database for 'BillingStatus'
      if (billingLookupType.length > 0) {
        this.billingStatusLookup = billingLookupType[0].Lookups;
        console.log('billingStatusLookup', this.billingStatusLookup);
      }
    });
    if (!this.manageMode) {
      uploadService.getFileUploads(this.globals.user.userName, true).subscribe(res => {
        this.uploadedFiles = res;
        console.log('Uploaded Files', this.uploadedFiles);
      });
    } else {
      uploadService.getTodayFileUploads(this.globals.user.userName, false).subscribe(res => {
        this.uploadedFiles = res;
        // console.log('Uploaded Files', this.uploadedFiles);
      });
    }
  }

  public refreshLog() {
    // console.log('refreshing Log 2');
    this.uploadService.getTodayFileUploads(this.globals.user.userName, false).subscribe(res => {
      this.uploadedFiles = res;
    });
  }

  setHeight() {
    return window.innerHeight - (window.innerHeight * .25);
  }

  onRowPrepared(e) {
    console.log('RowPrepared', e);
    if (e.rowType === 'data') {
        if (e.data.Archived) {
            console.log(e);
            e.rowElement.style.color = 'lightgrey';
        }
    }
}

  showDetails(fileUploadLog) {
    console.log('showDetails', fileUploadLog);
    this.batchDetailTitle = 'Batch '.concat(fileUploadLog.BatchName, ' Details', fileUploadLog.Archived ? '(Archived)' : '');
    this.currentBatch = fileUploadLog;
    this.loadBatchFiles(this.currentBatch.Id).subscribe(res => {
      this.paRequests = res;
      console.log('Loaded Batch Requests', this.paRequests);
      this.calculateStatistics();
      this.showPopup = true;
    });
  }

  loadBatchFiles(id: number) {
    return this.paRequestService.getBatchPaRequests(this.globals.user.userName, id)
      .pipe(map(res => {
        console.log('Return from getBatchPaRequests', res);
        return res;
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
      }));
  }

  calculateStatistics() {
    this.batchStatistics = [];
    this.billingStatistics = [];
    if (this.statusLookup) {
      this.statusLookup.forEach(element => {
        // console.log('lookup status listing', element.Id, element.LookupValue);
        const statDisplay = element.LookupValue;
        const newBatchStat: BatchStatistic = {
          Display: element.LookupValue,
          Count: this.paRequests.filter(p => p.Status === element.Id) == null
            ? 0
            : this.paRequests.filter(p => p.Status === element.Id).length
        };
        this.batchStatistics.push(newBatchStat);
      });
      const compBatchStat: BatchStatistic = {
        Display: 'Completed',
        Count: this.paRequests.filter(p => p.Completed === true) == null ? 0 : this.paRequests.filter(p => p.Completed === true).length
      };
      this.batchStatistics.push(compBatchStat);

    }
    if (this.billingStatusLookup) {
      this.billingStatusLookup.forEach(element => {
        const statDisplay = element.LookupValue;
        const newBatchStat: BatchStatistic = {
          Display: element.LookupValue,
          Count: this.paRequests.filter(p => p.BillingStatus === element.Id) == null
            ? 0
            : this.paRequests.filter(p => p.BillingStatus === element.Id).length
        };
        this.billingStatistics.push(newBatchStat);
      });
    }
    console.log('Loaded Statistics', this.batchStatistics);
    console.log('Billing Statistics', this.billingStatistics);
  }

  doArchiveBatch(restore: boolean = false) {
    this.paRequests.forEach(element => {
      element.Archived = !restore;
      this.paRequestService.savePaRequest(this.globals.user.userName, element).subscribe();
    });
    this.currentBatch.Archived = !restore;
    this.uploadService.saveFileUpload(this.globals.user.userName, this.currentBatch).subscribe(res => {
        console.log('doArchiveBatch:Success!', res);
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
    this.showPopup = false;
  }

  cancelArchiveBatch() {
    this.showPopup = false;
  }
  ngOnInit() {
  }

}
