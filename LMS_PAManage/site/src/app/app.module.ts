import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpModule, Headers, Response } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AppComponent } from './app.component';
import { NavbarComponent } from './_shared/navbar/navbar.component';
import { PaUploadComponent } from './pa-upload/pa-upload.component';
import { RouterModule, Routes } from '@angular/router';
import { UploaderComponent } from './_shared/uploader/uploader.component';

import { DxFileUploaderModule, DxTemplateModule, DevExtremeModule, DxTextAreaModule, DxButtonModule } from 'devextreme-angular';
import { PaManageComponent } from './pa-manage/pa-manage.component';
import { PaReportingComponent } from './pa-reporting/pa-reporting.component';
import { PaDetailComponent } from './pa-manage/pa-detail/pa-detail.component';
import { PaListComponent } from './pa-manage/pa-list/pa-list.component';
// import { LoginComponent } from './login/login.component';
// import { AuthGuard } from './guards/auth.guard';

const appRoutes: Routes = [
  { path: 'PAUpload', component: PaUploadComponent},
  { path: 'PAManage', component: PaManageComponent},
  { path: 'PAReports', component: PaReportingComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    PaUploadComponent,
    UploaderComponent,
    PaManageComponent,
    PaReportingComponent,
    PaDetailComponent,
    PaListComponent
    // LoginComponent
  ],
  imports: [NgbModule.forRoot(),
            RouterModule.forRoot(appRoutes, { enableTracing: true }),
            BrowserModule,
            BrowserAnimationsModule,
            HttpModule,
            DevExtremeModule,
            DxTemplateModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
