import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PerformanceInfo, UserPerformanceInfo } from '../../_services/pa-reporting.service';
import { PaRequest, BatchStatistic, PaRequestService } from '../../_services/pa-request.service';
import { LookupItem, LookupType, LookupService } from '../../_services/lookup.service';
import { User, UserService } from '../../_services/user.service';
import { Globals } from '../../globals';

@Component({
  selector: 'app-pa-rpt-user-status',
  templateUrl: './pa-rpt-user-status.component.html',
  styleUrls: ['./pa-rpt-user-status.component.sass'],
  providers: [PaRequestService, UserService, LookupService]
})
export class PaRptUserStatusComponent implements OnInit {
  userStatusData: UserPerformanceInfo[];
  statusLookup;
  billingStatusLookup;
  paRequests;
  users;


  constructor(private globals: Globals, public lookupService: LookupService,
    private paRequestService: PaRequestService, private userService: UserService,
    private _router: Router) {
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
      userService.getUsers(globals.user.userName, false).subscribe(res2 => {
        // this.users = res2.filter(p => p.Archived === false);
        this.users = res2;
        paRequestService.getPaRequests(globals.user.userName).subscribe(res3 => {
          this.paRequests = res3;
          this.calculateStatistics();
        });
      });
    });
    //    this.performanceData = paRptService.getMothlyPerformanceData();
  }

  calculateStatistics() {
    this.userStatusData = [];
    // console.log('paRequest', this.paRequests);
    if (this.users) {
      this.users.forEach(user => {
        // console.log('calc: currentUser', user);
        const usersPaData = this.paRequests.filter(p => p.AssignedTo === user.UserName);
        // console.log('userPaData', usersPaData);
        if (this.statusLookup) {
          this.statusLookup.forEach(element => {
            // console.log('lookup status listing', element.Id, element.LookupValue);
            const statDisplay = element.LookupValue;
            const newUserPerformance: UserPerformanceInfo = {
              user: user.FirstName + ' ' + user.LastName,
              status: element.LookupValue,
              count: usersPaData.filter(p => p.Status === element.Id) == null
                ? 0
                : usersPaData.filter(p => p.Status === element.Id).length
            };
            this.userStatusData.push(newUserPerformance);
          });
        }
      });
    }
    console.log('userStatusData', this.userStatusData);
  }
  customizeTooltip(arg: any) {
    return {
      text: arg.seriesName + ': ' + arg.valueText
    };
  }
  ngOnInit() {
  }

}
