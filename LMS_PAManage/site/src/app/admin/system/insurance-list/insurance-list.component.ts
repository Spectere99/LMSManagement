import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Globals } from '../../../globals';
import { DxSelectBoxModule,
  DxDataGridModule,
  DxDataGridComponent
} from 'devextreme-angular';
import { InsuranceCompany, InsuranceCompanyService } from '../../../_services/insurance-company.service';


@Component({
  selector: 'app-insurance-list',
  templateUrl: './insurance-list.component.html',
  styleUrls: ['./insurance-list.component.sass'],
  providers: [InsuranceCompanyService]
})
export class InsuranceListComponent implements OnInit {
  securityMessage = '';
  insuranceCompanies: InsuranceCompany[] = [];
  selectedInsuranceCompany: InsuranceCompany;

  constructor(private globals: Globals, private _router: Router, public insuranceCompanyService: InsuranceCompanyService) {
    this.insuranceCompanyService.getInsuranceCompanies(this.globals.user.userName).subscribe(res => {
      this.insuranceCompanies = res;
    });
  }

  onContentReady(e) {
    e.component.columnOption('command:edit', {
        visibleIndex: -1,
        width: 80
    });
}

setHeight() {
    return window.innerHeight - (window.innerHeight * .25);
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
initRecord(e) {
    // console.log('Adding Record', this.selectedInsuranceCompany);
    const addRec: InsuranceCompany = {
        Id: 0,
        CompanyCode: '',
        CompanyName: '',
        Created: new Date().toISOString(),
        CreatedBy: this.globals.user.userName,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
        Archived: false
    };
    e.data = addRec;
}

addRecord(d) {
    // console.log('Adding Record', d);
    const updRec: InsuranceCompany = {
        Id: 0,
        CompanyCode: '',
        CompanyName: '',
        Created: d.data.Created,
        CreatedBy: d.data.CreatedBy,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
        Archived: d.data.Archived
    };
    this.insuranceCompanyService.saveInsuranceCompany(this.globals.user.userName, updRec).subscribe(res => {
    }, (error) => {
        if (error.status === 401) {
            this.securityMessage = error._body;
            console.error('insurance-list:addRecord (auth error)', error);
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
    const updRec: InsuranceCompany = {
        Id: d.key.Id,
        CompanyCode: d.newData.CompanyCode === undefined ? d.oldData.CompanyCode : d.newData.CompanyCode,
        CompanyName: d.newData.CompanyName === undefined ? d.oldData.CompanyName : d.newData.CompanyName,
        Created: d.newData.Created === undefined ? d.oldData.Created : d.newData.Created ,
        CreatedBy: d.newData.CreatedBy === undefined ? d.oldData.CreatedBy : d.newData.CreatedBy ,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
        Archived: d.newData.Archived === undefined ? d.oldData.Archived : d.newData.Archived
    };

    this.insuranceCompanyService.saveInsuranceCompany(this.globals.user.userName, updRec).subscribe(res => {
    }, (error) => {
        if (error.status === 401) {
            this.securityMessage = error._body;
            console.error('insurance-list:updateRecord (auth error)', error);
            setTimeout(() => {
                console.log('redirecting');
                this.globals.isAuthenticated = false;
                this._router.navigate(['/Login']);
            }, 10000);
        }
    });
}

  ngOnInit() {
  }

}
