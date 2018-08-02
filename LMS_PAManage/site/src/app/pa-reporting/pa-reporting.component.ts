import { Component, OnInit } from '@angular/core';
import {registerPalette, currentPalette} from 'devextreme/viz/palette';

@Component({
  selector: 'app-pa-reporting',
  templateUrl: './pa-reporting.component.html',
  styleUrls: ['./pa-reporting.component.sass'],
  providers: []
})
export class PaReportingComponent implements OnInit {

  reportingPalette = {
    simpleSet: ['#555555', '#FFB6C1', '#ADD8E6', '#DDA0DD', '#D3D3D3', '#008000', '#FF0000', '#FFA500']
  };

  constructor() {
    // this.performanceData = paRptService.getMothlyPerformanceData();
    // this.dailyProductionData = paRptService.GetDailyPerformanceData();
    registerPalette('reportingPalette', this.reportingPalette);
    currentPalette('reportingPalette');
   }
  ngOnInit() {
  }

}
