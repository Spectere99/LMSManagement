import { Component, OnInit } from '@angular/core';
import { PerformanceInfo, DailyPerformanceInfo, PAReportingService } from '../_services/pa-reporting.service';

@Component({
  selector: 'app-pa-reporting',
  templateUrl: './pa-reporting.component.html',
  styleUrls: ['./pa-reporting.component.sass'],
  providers: [ PAReportingService ]
})
export class PaReportingComponent implements OnInit {
performanceData: PerformanceInfo[];
dailyProductionData: DailyPerformanceInfo[];
  constructor(paRptService: PAReportingService) {
    this.performanceData = paRptService.getMothlyPerformanceData();
    this.dailyProductionData = paRptService.GetDailyPerformanceData();
   }

   customizeLabel(arg) {
    return arg.valueText + ' (' + arg.percentText + ')';
}
  ngOnInit() {
  }

}
