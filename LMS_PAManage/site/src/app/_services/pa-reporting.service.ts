import {
    Injectable
} from '@angular/core';

export class PerformanceInfo {
    status: string;
    count: number;
}
export class UserPerformanceInfo {
    user: string;
    status: string;
    count: number;
}


@Injectable()
export class PAReportingService {
}
