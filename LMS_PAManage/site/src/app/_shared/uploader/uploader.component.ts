import { Component, OnInit, ViewChild } from '@angular/core';
import {DxButtonModule} from 'devextreme-angular';
import { environment } from '../../../environments/environment';
import { FileUploadLog, FileUploadService} from '../../_services/file-upload.service';
import { FileUploadLogComponent } from '../file-upload-log/file-upload-log.component';

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.sass'],
  providers: [FileUploadService]
})
export class UploaderComponent implements OnInit {
  @ViewChild(FileUploadLogComponent) fileUploadLog;
  uploadURL = environment.baseURL + '/api/Upload';
  value: any[] = [];
  clearButtonText = 'Clear Queue';
  uploadHeaders = {
    userid: 'rwflowers'
  };

  constructor() {  }

  clearQueue() {
    this.value = [];
  }

  uploadComplete(e) {
    console.log('Refreshing Log');
    this.fileUploadLog.refreshLog();
  }

  ngOnInit() {
  }

}
