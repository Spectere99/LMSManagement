import { Injectable } from '@angular/core';

export class PrescriptionAuthorization {
    id: number;
    submitDate: string;
    physicianName: string;
    patientName: string;
    drugName: string;
    insurance: string;
    approvalStatus: string;
    approvalDate: string;
    approvalDocLink: string;
    denialDate: string;
    notes: string;
    createdBy: string;
}

const authorizations: PrescriptionAuthorization[] =  [
    {
        'id': 1,
        'submitDate': '2018/05/11',
        'physicianName': 'Maxine Reynolds',
        'patientName': 'Josephine Harper',
        'drugName': 'LIDOCAIN 5% PATCH',
        'insurance': 'UHC',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'ea mollit duis',
        'createdBy': 'AM'
    },
    {
        'id': 2,
        'submitDate': '2018/05/11',
        'physicianName': 'Joseph Chandler',
        'patientName': 'Noel Blackwell',
        'drugName': 'ORTHO D 3775 IU/1 MG CAP',
        'insurance': 'BCBS',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'qui nulla elit',
        'createdBy': 'RF'
    },
    {
        'id': 3,
        'submitDate': '2018/05/11',
        'physicianName': 'Maxine Reynolds',
        'patientName': 'Mayer Pierce',
        'drugName': 'ZYVIT CAPSULES',
        'insurance': 'UHC',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'magna mollit in',
        'createdBy': 'SK'
    },
    {
        'id': 4,
        'submitDate': '2018/05/11',
        'physicianName': 'Joseph Chandler',
        'patientName': 'Pace Knowles',
        'drugName': 'ZYVIT CAPSULES',
        'insurance': 'BCBS',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'laborum nulla proident',
        'createdBy': 'RF'
    },
    {
        'id': 5,
        'submitDate': '2018/05/12',
        'physicianName': 'Joseph Chandler',
        'patientName': 'Sue Bradshaw',
        'drugName': 'ZYVIT CAPSULES',
        'insurance': 'UHC',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'velit consectetur pariatur',
        'createdBy': 'AM'
    },
    {
        'id': 6,
        'submitDate': '2018/05/12',
        'physicianName': 'Maxine Reynolds',
        'patientName': 'Holly Chase',
        'drugName': 'LIDOCAIN 5% PATCH',
        'insurance': 'UHC',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'duis exercitation excepteur',
        'createdBy': 'AM'
    },
    {
        'id': 7,
        'submitDate': '2018/05/12',
        'physicianName': 'Joseph Chandler',
        'patientName': 'Ramirez Wheeler',
        'drugName': 'DICLOFENAC 1.5% DROPS',
        'insurance': 'UHC',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'dolore officia ea',
        'createdBy': 'SK'
    },
    {
        'id': 8,
        'submitDate': '2018/05/14',
        'physicianName': 'Joseph Chandler',
        'patientName': 'Ramirez Wheeler',
        'drugName': 'DICLOFENAC 1.5% DROPS',
        'insurance': 'UHC',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'dolore officia ea',
        'createdBy': 'SK'
    },
    {
        'id': 9,
        'submitDate': '2018/05/14',
        'physicianName': 'Joseph Chandler',
        'patientName': 'Ramirez Wheeler',
        'drugName': 'SINGULAIR 250MG',
        'insurance': 'UHC',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'dolore officia ea',
        'createdBy': 'SK'
    },
    {
        'id': 10,
        'submitDate': '2018/05/14',
        'physicianName': 'Joseph Chandler',
        'patientName': 'Ramirez Wheeler',
        'drugName': 'DICLOFENAC 2.5% DROPS',
        'insurance': 'UHC',
        'approvalStatus': null,
        'approvalDate': null,
        // tslint:disable-next-line:max-line-length
        'approvalDocLink': 'https://netorgft2032167-my.sharepoint.com/personal/victoria_lmshealthpro_com/Documents/ALEX/AETNA/03-30-2018-Mobley,%20Ralph-Lidocaine%20Patch.pdf',
        'denialDate': null,
        'notes': 'dolore officia ea',
        'createdBy': 'SK'
    }
];

@Injectable()
export class PAAuthService {
    getAuthorizations() {
        return authorizations;
    }
}
