import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PerformanceInfo, UserPerformanceInfo, PAReportingService } from '../../_services/pa-reporting.service';
import { PaRequest, BatchStatistic, PaRequestService } from '../../_services/pa-request.service';
import { LookupItem, LookupType, LookupService } from '../../_services/lookup.service';
import { Globals } from '../../globals';

@Component({
  selector: 'app-pa-rpt-status',
  templateUrl: './pa-rpt-status.component.html',
  styleUrls: ['./pa-rpt-status.component.sass'],
  providers: [PAReportingService, PaRequestService, LookupService]
})

export class PaRptStatusComponent implements OnInit {
performanceData: PerformanceInfo[];
statusLookup;
billingStatusLookup;
paRequests;
billingStatistics: BatchStatistic[];

  constructor(paRptService: PAReportingService, private globals: Globals, public lookupService: LookupService,
        private paRequestService: PaRequestService,
    private _router: Router) {
    lookupService.getLookupTypes(globals.user.userName).subscribe(res => {
      const requestLookupType = res.filter(p => p.Id === 3); // Pre seeded database for 'Pa_RequestStatus'
      if (requestLookupType.length > 0) {
        this.statusLookup = requestLookupType[0].Lookups;
        // console.log('statusLookup', this.statusLookup);
      }
      const billingLookupType = res.filter(p => p.Id === 4); // Pre seeded database for 'BillingStatus'
      if (billingLookupType.length > 0) {
        this.billingStatusLookup = billingLookupType[0].Lookups;
        // console.log('billingStatusLookup', this.billingStatusLookup);
      }
      paRequestService.getPaRequests(globals.user.userName).subscribe(res2 => {
        this.paRequests = res2;
        this.calculateStatistics();
      });
    });
//    this.performanceData = paRptService.getMothlyPerformanceData();
   }

   customizeLabel(arg) {
    console.log('customerLabel', arg);
   return arg.argument + ' (' + arg.valueText + ') -' + arg.percentText;
}


calculateStatistics() {
  this.performanceData = [];
  this.billingStatistics = [];
  if (this.statusLookup) {
    this.statusLookup.forEach(element => {
      // console.log('lookup status listing', element.Id, element.LookupValue);
      const statDisplay = element.LookupValue;
      const newBatchStat: PerformanceInfo = {
        status: element.LookupValue,
        count: this.paRequests.filter(p => p.Status === element.Id) == null
          ? 0
          : this.paRequests.filter(p => p.Status === element.Id).length
      };
      this.performanceData.push(newBatchStat);
    });
    /* const compBatchStat: BatchStatistic = {
      Display: 'Completed',
      Count: this.paRequests.filter(p => p.Completed === true) == null ? 0 : this.paRequests.filter(p => p.Completed === true).length
    };
    this.performanceData.push(compBatchStat); */

  }
  /* if (this.billingStatusLookup) {
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
  } */
  // console.log('Loaded Statistics', this.performanceData);
  // console.log('Billing Statistics', this.billingStatistics);
}
  ngOnInit() {
  }

}
