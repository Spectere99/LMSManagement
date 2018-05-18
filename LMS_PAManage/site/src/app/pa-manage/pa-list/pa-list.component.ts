import { Component, OnInit } from '@angular/core';
import { DxDataGridModule,
    DxDataGridComponent } from 'devextreme-angular';
import { PrescriptionAuthorization, PAAuthService } from '../../_services/pa.service';

@Component({
  selector: 'app-pa-list',
  templateUrl: './pa-list.component.html',
  styleUrls: ['./pa-list.component.sass'],
  providers: [PAAuthService]
})
export class PaListComponent implements OnInit {
authorizations: PrescriptionAuthorization[];
insuranceLookup: any[];
statusLookup: any[];

  constructor(paAuthService: PAAuthService) {
    this.authorizations = paAuthService.getAuthorizations();
    this.insuranceLookup = [{
            abbr: 'UHC',
            name: 'United Healthcare'
        },
        {
            abbr: 'BCBS',
            name: 'Blue Cross/Blue Shield'
        },
        {
            abbr: 'MC',
            name: 'Medicare'
        },
        {
            abbr: 'AET',
            name: 'Aetna'
        },
        {
            abbr: 'HUM',
            name: 'Humana'
        }];
    this.statusLookup = [{
            statusCode: 'ip',
            status: 'Assigned'
        },
        {
            statusCode: 'sub',
            status: 'Pending'
        },
        {
            statusCode: 'd',
            status: 'Denied'
        },
        {
            statusCode: 'app',
            status: 'Approved'
        },
        {
            statusCode: 'oh',
            status: 'Appealed'
        }];
   }
   onContentReady(e) {
    e.component.columnOption('command:edit', {
       visibleIndex: -1,
       width: 80
   });
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
  ngOnInit() {
  }

}
