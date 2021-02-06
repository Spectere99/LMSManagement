import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import {DxButtonModule, DxRadioGroupComponent} from 'devextreme-angular';
import { Globals } from '../../globals';
import { environment } from '../../../environments/environment';
import { FileUploadLog, FileUploadService} from '../../_services/file-upload.service';
import { FileUploadLogComponent } from '../file-upload-log/file-upload-log.component';
import { LookupService, LookupItem } from '../../_services/lookup.service';

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.sass'],
  providers: [FileUploadService,
              LookupService]
})
export class UploaderComponent implements AfterViewInit {
  @ViewChild(FileUploadLogComponent) fileUploadLog;
  @ViewChild('eventRadioGroup') eventRadioGroup: DxRadioGroupComponent;
  uploadURL = environment.baseURL + '/api/Upload';

  modules: string[];
  module: string;
  lookups: LookupItem[];

  value: any[] = [];
  currentModuleId = 1;
  clearButtonText = 'Clear Queue';
  uploadHeaders = {
    userid: this.globals.user.userName,
    module: this.currentModuleId
  };
  constructor(public globals: Globals, private lookupService: LookupService) {
    this.lookupService.getLookupsByType(globals.user.userId, 2)
    .subscribe(res => {
        this.modules = res;
        console.log(this.modules);
    });
  }

  clearQueue() {
    this.value = [];
  }
  onValueChanged(e) {
    console.log('onValueChanged', e);
    this.currentModuleId = e.value;
}
  uploadComplete(e) {
    // console.log('Refreshing Log');
    this.fileUploadLog.refreshLog();
  }

  ngAfterViewInit() {
    // this.eventRadioGroup.instance.option('value', this.modules[0]);
  }

}
