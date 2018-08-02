import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Globals } from '../../../globals';
import { DxSelectBoxModule,
  DxDataGridModule,
  DxDataGridComponent
} from 'devextreme-angular';
import { LookupType, LookupItem, LookupService } from '../../../_services/lookup.service';

@Component({
  selector: 'app-lookup-list',
  templateUrl: './lookup-list.component.html',
  styleUrls: ['./lookup-list.component.sass'],
  providers: [LookupService]
})
export class LookupListComponent implements OnInit {
securityMessage = '';
lookupTypes: LookupType[] = [];
lookups: LookupItem[] = [];
selectedLookupType: LookupType;

  constructor(private globals: Globals, private _router: Router, public lookupService: LookupService) {
    this.lookupService.getLookupTypes(this.globals.user.userName).subscribe(res => {
      this.lookupTypes = res;
    });
  }

  loadLookups(e) {
    console.log(e.selectedItem);
    this.selectedLookupType = e.selectedItem;
    this.lookups = e.selectedItem.Lookups;
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
    console.log('Adding Record', this.selectedLookupType);
    const addRec: LookupItem = {
        Id: 0,
        LookupTypeId: this.selectedLookupType.Id,
        LookupValue: '',
        Created: new Date().toISOString(),
        CreatedBy: this.globals.user.userName,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
        Archived: false
    };
    e.data = addRec;
}

addRecord(d) {
    console.log('Adding Record', d);
    const updRec: LookupItem = {
        Id: 0,
        LookupTypeId: d.data.LookupTypeId,
        LookupValue: d.data.LookupValue,
        Created: d.data.Created,
        CreatedBy: d.data.CreatedBy,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
        Archived: d.data.Archived
    };
    this.lookupService.saveLookupItem(this.globals.user.userName, updRec).subscribe(res => {
    }, (error) => {
        if (error.status === 401) {
            this.securityMessage = error._body;
            console.error('lookup-list:addRecord (auth error)', error);
            setTimeout(() => {
                console.log('redirecting');
                this.globals.isAuthenticated = false;
                this._router.navigate(['/Login']);
            }, 10000);
        }
    });
}

updateRecord(d) {
    console.log('Saving Record', d);
    const updRec: LookupItem = {
        Id: d.key.Id,
        LookupTypeId: d.newData.LookupTypeId === undefined ? d.oldData.LookupTypeId : d.newData.LookupTypeId,
        LookupValue: d.newData.LookupValue === undefined ? d.oldData.LookupValue : d.newData.LookupValue,
        Created: d.newData.Created === undefined ? d.oldData.Created : d.newData.Created ,
        CreatedBy: d.newData.CreatedBy === undefined ? d.oldData.CreatedBy : d.newData.CreatedBy ,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
        Archived: d.newData.Archived === undefined ? d.oldData.Archived : d.newData.Archived
    };

    this.lookupService.saveLookupItem(this.globals.user.userName, updRec).subscribe(res => {
    }, (error) => {
        if (error.status === 401) {
            this.securityMessage = error._body;
            console.error('lookup-list:updateRecord (auth error)', error);
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
