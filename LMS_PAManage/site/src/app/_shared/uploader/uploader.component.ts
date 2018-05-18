import { Component, OnInit } from '@angular/core';
import {DxButtonModule} from 'devextreme-angular';
import { FileUploadLog, FileUploadService} from '../../_services/file-upload.service';

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.sass'],
  providers: [FileUploadService]
})
export class UploaderComponent implements OnInit {
  value: any[] = [];
  uploadedFiles;
  clearButtonText = 'Clear Queue';
  uploadHeaders = {
    userid: 'rwflowers'
  };

  constructor(public uploadService: FileUploadService) {

    //  uploadService.getFileUploads('rwflowers').subscribe((res: Response) => this.uploadedFiles = res);
    uploadService.getFileUploads('rwflowers').subscribe(res => {
      this.uploadedFiles = res;
      console.log(this.uploadedFiles);
    });
  }

  clearQueue() {
    this.value = [];
  }

  uploadComplete(e) {

  }

  ngOnInit() {
  }

}
