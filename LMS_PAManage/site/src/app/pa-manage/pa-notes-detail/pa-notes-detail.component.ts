import { AfterViewInit, Component, Input, OnInit, ViewChildren } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import {PaRequestNote, PaRequestNoteService} from '../../_services/pa-request-note.service';
import { Globals } from '../../globals';
import { AuthenticationService } from '../../_services/authentication.service';
import { Router } from '@angular/router';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
  selector: 'app-pa-notes-detail',
  templateUrl: './pa-notes-detail.component.html',
  styleUrls: ['./pa-notes-detail.component.sass'],
  providers: [PaRequestNoteService]
})
export class PaNotesDetailComponent implements AfterViewInit {
  @Input() key: number;
  @ViewChildren(DxDataGridComponent) dataGrid: DxDataGridComponent;
  paRequestNotes: PaRequestNote[] = [];
  securityMessage = '';


    constructor(public globals: Globals, private _router: Router, private paRequestNoteService: PaRequestNoteService) {
    }

    pullRequestNotes() {
      console.log('pullRequestNotes', this.key);
      this.paRequestNoteService.getPaRequestNotes(this.key)
          .subscribe(res => {
              this.paRequestNotes = res;
              console.log(this.paRequestNotes);
          }, (error) => {
              if (error.status === 401) {
                  this.securityMessage = error._body;
                  console.error('Pa-notes-detail:pullRequestNotes (auth error)', error);
                  setTimeout(() => {
                      console.log('redirecting');
                      this.globals.isAuthenticated = false;
                      this._router.navigate(['/Login']);
                  }, 10000);
              }
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
  onEditorPreparing(e: any) {
    if (e.parentType === 'dataRow' && e.dataField === 'Note') {
        e.editorName = 'dxTextArea';
    }
  }
  initRecord(e) {
    // console.log('InitRecord', this.insuranceLookup);
    const addRec: PaRequestNote = {
        Id: 0,
        PaRequestId: this.key,
        IsPublic: false,
        NoteText: null,
        Archived: false,
        Created: new Date().toISOString(),
        CreatedBy: this.globals.user.userName,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
    };
    e.data = addRec;
}

addRecord(d) {
    // console.log('Adding Record', d);
    const newRec: PaRequestNote = {
        Id: 0,
        PaRequestId: d.data.PaRequestId === undefined ? this.key : d.data.PaRequestId,
        IsPublic: d.data.IsPublic === undefined ? false : d.data.IsPublic,
        NoteText: d.data.NoteText === undefined ? null : d.data.NoteText,
        Archived: d.data.Archived === undefined ? false : d.data.Archived,
        Created: d.data.Created === undefined ? new Date().toISOString() : d.data.Created,
        CreatedBy: d.data.CreatedBy === undefined ? this.globals.user.userName : d.data.CreatedBy,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
    };

    this.paRequestNoteService.savePaRequestNote(this.globals.user.userName, newRec)
        .subscribe(res => { d.component.refresh();
        }, (error) => {
            if (error.status === 401) {
                this.securityMessage = error._body;
                console.error('Pa-list:updateRecord (auth error)', error);
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
    const updRec: PaRequestNote = {
        Id: d.key.Id,
        PaRequestId: d.newData.PaRequestId === undefined ? d.oldData.PaRequestId : d.newData.PaRequestId,
        IsPublic: d.newData.IsPublic === undefined ? d.oldData.IsPublic : d.newData.IsPublic,
        NoteText: d.newData.NoteText === undefined ? d.oldData.NoteText : d.newData.NoteText,
        Archived: d.newData.Archived === undefined ? d.oldData.Archived : d.newData.Archived,
        Created: d.newData.Created === undefined ? d.oldData.Created : d.newData.Created,
        CreatedBy: d.newData.CreatedBy === undefined ? d.oldData.CreatedBy : d.newData.CreatedBy,
        LastModified: new Date().toISOString(),
        LastModifiedBy: this.globals.user.userName,
    };

    // console.log('updRec', updRec);

    this.paRequestNoteService.savePaRequestNote(this.globals.user.userName, updRec)
        .subscribe(res => { d.component.refresh();
        }, (error) => {
            if (error.status === 401) {
                this.securityMessage = error._body;
                console.error('Pa-notes-detail:updateRecord (auth error)', error);
                setTimeout(() => {
                    console.log('redirecting');
                    this.globals.isAuthenticated = false;
                    this._router.navigate(['/Login']);
                }, 10000);
            }
        });
}
    ngAfterViewInit() {
      this.pullRequestNotes();
    }

}
