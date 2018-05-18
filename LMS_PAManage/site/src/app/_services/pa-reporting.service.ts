import {
    Injectable
} from '@angular/core';

export class PerformanceInfo {
    createdBy: string;
    count: number;
}
export class DailyPerformanceInfo {
    day: string;
    am: number;
    sk: number;
    rf: number;
}

const performance: PerformanceInfo[] = [{
    createdBy: 'RF',
    count: 110
}, {
    createdBy: 'SK',
    count: 200
}, {
    createdBy: 'AM',
    count: 350
}];

const dailyPerformance: DailyPerformanceInfo[] = [{
    day: '5/1/18',
    am: 5,
    sk: 10,
    rf: 7
},
{
    day: '5/2/18',
    am: 15,
    sk: 7,
    rf: 17
},
{
    day: '5/3/18',
    am: 0,
    sk: 7,
    rf: 10
},
{
    day: '5/4/18',
    am: 7,
    sk: 13,
    rf: 5
},
{
    day: '5/5/18',
    am: 15,
    sk: 10,
    rf: 7
},
{
    day: '5/6/18',
    am: 4,
    sk: 15,
    rf: 5
},
{
    day: '5/7/18',
    am: 20,
    sk: 2,
    rf: 9
},
{
    day: '5/8/18',
    am: 1,
    sk: 19,
    rf: 10
},
{
    day: '5/9/18',
    am: 5,
    sk: 2,
    rf: 3
},
{
    day: '5/10/18',
    am: 2,
    sk: 4,
    rf: 4
},
{
    day: '5/11/18',
    am: 2,
    sk: 15,
    rf: 9
},
{
    day: '5/12/18',
    am: 15,
    sk: 9,
    rf: 16
},
{
    day: '5/13/18',
    am: 14,
    sk: 2,
    rf: 7
},
{
    day: '5/14/18',
    am: 9,
    sk: 11,
    rf: 6
},
{
    day: '5/15/18',
    am: 12,
    sk: 20,
    rf: 1
}];

@Injectable()
export class PAReportingService {
    getMothlyPerformanceData(): PerformanceInfo[] {
        return performance;
    }

    GetDailyPerformanceData(): DailyPerformanceInfo[] {
        return dailyPerformance;
    }
}
