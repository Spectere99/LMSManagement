import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpModule, Headers, Response } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AppComponent } from './app.component';
import { Globals } from './globals';
import { WindowRef } from './_services/window-ref.service';

import { PhoneFormat } from './_shared/pipes/phone.pipe';

import { NavbarComponent } from './_shared/navbar/navbar.component';
import { PaUploadComponent } from './pa-upload/pa-upload.component';
import { RouterModule, Routes } from '@angular/router';
import { UploaderComponent } from './_shared/uploader/uploader.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { DxFileUploaderModule, DxTemplateModule, DevExtremeModule, DxTextAreaModule, DxButtonModule } from 'devextreme-angular';
import {MatCardModule, MatIconModule, MatListModule, MatTabsModule, MatTooltipModule,
        MatCheckboxModule} from '@angular/material';

import { PaManageComponent } from './pa-manage/pa-manage.component';
import { PaReportingComponent } from './pa-reporting/pa-reporting.component';
import { PaDetailComponent } from './pa-manage/pa-detail/pa-detail.component';
import { PaListComponent } from './pa-manage/pa-list/pa-list.component';
import { FileUploadLogComponent } from './_shared/file-upload-log/file-upload-log.component';
import { AdminComponent } from './admin/admin.component';
import { UserSecurityComponent } from './admin/user-security/user-security.component';
import { SystemComponent } from './admin/system/system.component';
import { LookupListComponent } from './admin/system/lookup-list/lookup-list.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthenticationService } from './_services/authentication.service';
import { InsuranceListComponent } from './admin/system/insurance-list/insurance-list.component';
import { UserListComponent } from './admin/user-security/user-list/user-list.component';
import { RoleListComponent } from './admin/user-security/role-list/role-list.component';
import { UserLoginListComponent } from './admin/user-security/user-login-list/user-login-list.component';
import { BatchManageComponent } from './admin/batch-manage/batch-manage.component';
import { PaRptStatusComponent } from './pa-reporting/pa-rpt-status/pa-rpt-status.component';
import { PaRptUserStatusComponent } from './pa-reporting/pa-rpt-user-status/pa-rpt-user-status.component';
import { PaNotesDetailComponent } from './pa-manage/pa-notes-detail/pa-notes-detail.component';

const appRoutes: Routes = [
  { path: 'Login', component: LoginComponent },
  { path: 'PAUpload', component: PaUploadComponent, canActivate: [AuthGuard]},
  { path: 'PAManage', component: PaManageComponent, canActivate: [AuthGuard]},
  { path: 'PAReports', component: PaReportingComponent, canActivate: [AuthGuard]},
  { path: 'BatchManage', component: BatchManageComponent, canActivate: [AuthGuard]},
  { path: 'UserAdmin', component: UserSecurityComponent, canActivate: [AuthGuard]},
  { path: 'SystemAdmin', component: SystemComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: 'PAManage', pathMatch: 'full'}
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
    PaListComponent,
    FileUploadLogComponent,
    AdminComponent,
    UserSecurityComponent,
    SystemComponent,
    LookupListComponent,
    LoginComponent,
    InsuranceListComponent,
    UserListComponent,
    RoleListComponent,
    UserLoginListComponent,
    PhoneFormat,
    BatchManageComponent,
    PaRptStatusComponent,
    PaRptUserStatusComponent,
    PaNotesDetailComponent
  ],
  imports: [NgbModule.forRoot(),
            RouterModule.forRoot(appRoutes), // , { enableTracing: true }),
            BrowserModule,
            BrowserAnimationsModule,
            HttpModule,
            FormsModule,
            ReactiveFormsModule,
            DevExtremeModule,
            DxTemplateModule,
            MatCardModule,
            MatIconModule,
            MatListModule,
            MatTabsModule,
            MatTooltipModule,
            MatCheckboxModule
  ],
  providers: [Globals, AuthGuard, AuthenticationService,
              WindowRef],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
 export function getWindow() { return window; }
