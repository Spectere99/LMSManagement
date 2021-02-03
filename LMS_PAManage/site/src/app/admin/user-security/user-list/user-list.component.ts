import { Component, ViewChild, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Globals } from '../../../globals';
import {
    DxSelectBoxModule,
    DxDataGridModule,
    DxDataGridComponent,
    DxPopupModule
} from 'devextreme-angular';
import notify from 'devextreme/ui/notify';

import { User, UserService } from '../../../_services/user.service';
import { UserLoginCreation, UserLoginService } from '../../../_services/user-login.service';
import { Role, RoleService } from '../../../_services/role.service';

import { RegistrationValidator } from '../../../_shared/register.validator';


@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.sass'],
    providers: [UserService, RoleService, UserLoginService]
})

export class UserListComponent implements OnInit {
    @ViewChild(DxDataGridComponent) userGrid: DxDataGridComponent;
    securityMessage = '';
    users: User[] = [];
    roleList: Role[] = [];
    selectedUser: User;
    editing = false;
    newUser = false;
    passwordReset = false;
    newPasswordValue = '';

    passwordFormGroup: FormGroup;

    constructor(private globals: Globals, private _router: Router, public userService: UserService,
        public roleService: RoleService, private userLoginService: UserLoginService,
        private formBuilder: FormBuilder) {

        this.passwordFormGroup = this.formBuilder.group({
            password: ['', Validators.required],
            repeatPassword: ['', Validators.required]
        }, {
                validator: RegistrationValidator.validate.bind(this)
            });

        this.loadUserList();

        this.roleService.getRoles(this.globals.user.userName).subscribe(res => {
            this.roleList = res;
            // console.log('RoleTypes', this.roleList);
        });
    }

    loadUserList() {
        this.userService.getUsers(this.globals.user.userName, true).subscribe(res => {
            this.users = res;
        });
    }
    onContentReady(e) {
        e.component.columnOption('command:edit', {
            visibleIndex: -1,
            width: 80
        });
    }
    resetPassword() {
        this.passwordReset = true;
    }

    cancelPasswordReset() {
        this.passwordReset = false;
    }

    doPasswordReset() {
        const userLoginCreation: UserLoginCreation = {
            Id: this.selectedUser.UserLogin.Id,
            IsAdmin: this.selectedUser.UserLogin.IsAdmin,
            Login: this.selectedUser.UserLogin.Login,
            Password: this.newPasswordValue
        };
        // console.log('userLoginCreation', userLoginCreation);

        this.userLoginService.saveUserLoginCreation(this.globals.user.userName, userLoginCreation).subscribe(res2 => {
            // console.log('return from saveUserLoginCreation', res2);
            this.selectedUser.UserLogin = res2;
            notify('Password Reset', 'success', 1000);
            this.passwordReset = false;
            // this.loadUserList();
        }, (error) => {
            notify(error.body, 'error', 1000);
            this.securityMessage = error.body;
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
    onRowPrepared(e) {
        if (e.rowType === 'data') {
            if (e.data.UserLogin.LockoutEnabled) {
                // console.log(e);
                e.rowElement.style.color = 'red';
            }
        }
    }
    onRowClick(e) {
        // console.log('Row Click', e);
        this.selectedUser = Object.assign({}, e.data);
        // console.log('Selected User', this.selectedUser);
        this.editing = true;
        this.newUser = false;
        // console.log('editing Existing User (newUser / passwordFormGroup.value)', this.newUser, this.passwordFormGroup.valid);
    }
    onRoleChange(e) {
        // console.log('onRoleChange', e);
        this.selectedUser.RoleId = e;
        const selectedRole = this.roleList.filter(p => p.Id === e);
        if (selectedRole && selectedRole.length > 0) {
            this.selectedUser.Role = selectedRole[0];
            this.selectedUser.UserLogin.IsAdmin = this.selectedUser.RoleId === 1 || this.selectedUser.RoleId === 2;
        }
        // console.log('onRoleChange:SelectedUser', this.selectedUser);
    }

    initRecord(e) {
        const addRec: User = {
            Id: 0,
            UserName: '',
            FirstName: '',
            LastName: '',
            Email: '',
            PhoneNumber: '',
            RoleId: this.roleList.filter(p => p.Name.toUpperCase() === 'STANDARD USER').length > 0
                        ? this.roleList.filter(p => p.Name.toUpperCase() === 'STANDARD USER')[0].Id : null,
            UserLoginId: 0,
            Created: new Date().toISOString(),
            CreatedBy: this.globals.user.userName,
            LastModified: new Date().toISOString(),
            LastModifiedBy: this.globals.user.userName,
            Archived: false,
            UserLogin: {
                Id: 0,
                IsAdmin: false,
                AccessFailedCount: 0,
                Created: new Date().toISOString(),
                CreatedBy: this.globals.user.userName,
                LastModified: new Date().toISOString(),
                LastModifiedBy: this.globals.user.userName,
                LockoutEnabled: false,
                LockoutEnd: new Date('1/1/1900').toISOString(),
                Login: '',
                PasswordHash: '',
                RefreshId: 0
            },
            Role: this.roleList.filter(p => p.Name.toUpperCase() === 'STANDARD USER').length > 0
                                        ? this.roleList.filter(p => p.Name.toUpperCase() === 'STANDARD USER')[0] : null
        };
        // e.data = addRec;
        this.editing = true;
        this.newUser = true;
        this.selectedUser = addRec;
        // console.log('Init Record', this.selectedUser);
    }

    addRecord(d) {
        // console.log('Adding Record', d);
        const updRec: User = {
            Id: 0,
            UserName: d.data.UserName,
            FirstName: d.data.FirstName,
            LastName: d.data.LastName,
            Email: d.data.Email,
            PhoneNumber: d.data.PhoneNumber,
            Created: d.data.Created,
            CreatedBy: d.data.CreatedBy,
            LastModified: new Date().toISOString(),
            LastModifiedBy: this.globals.user.userName,
            Archived: d.data.Archived,
            RoleId: 0,
            UserLoginId: 0,
            UserLogin: {
                Id: 0,
                IsAdmin: false,
                AccessFailedCount: 0,
                Created: new Date().toISOString(),
                CreatedBy: this.globals.user.userName,
                LastModified: new Date().toISOString(),
                LastModifiedBy: this.globals.user.userName,
                LockoutEnabled: false,
                LockoutEnd: new Date('1/1/1900').toISOString(),
                Login: d.data.UserName,
                PasswordHash: '',
                RefreshId: 0
            },
            Role: this.roleList.filter(p => p.Name.toUpperCase() === 'STANDARD USER').length > 0
                                        ? this.roleList.filter(p => p.Name.toUpperCase() === 'STANDARD USER')[0] : null
        };

        this.userService.saveUser(this.globals.user.userName, updRec).subscribe(res => {
        }, (error) => {
            if (error.status === 401) {
                this.securityMessage = error._body;
                console.error('user-list:addRecord (auth error)', error);
                setTimeout(() => {
                    console.log('redirecting');
                    this.globals.isAuthenticated = false;
                    this._router.navigate(['/Login']);
                }, 10000);
            }
        });
    }

    udpateRecord() {
        // console.log('Saving Record', this.selectedUser);
        this.selectedUser.UserLogin.Login = this.selectedUser.UserName;
        this.userService.saveUser(this.globals.user.userName, this.selectedUser).subscribe(res => {
            // console.log('Return from saveUser', res);
            const userLoginCreation: UserLoginCreation = {
                Id: res.UserLogin.Id,
                IsAdmin: res.UserLogin.IsAdmin,
                Login: res.UserLogin.Login,
                Password: this.passwordFormGroup.get('password').value
            };
            // console.log('userLoginCreation', userLoginCreation);

            this.userLoginService.saveUserLoginCreation(this.globals.user.userName, userLoginCreation).subscribe(res2 => {
                // console.log('return from saveUserLoginCreation', res2);
                this.selectedUser.UserLogin = res2;
                this.editing = false;
                this.newUser = false;
                notify('User Records Saved', 'success', 1000);
                this.loadUserList();
            }, (error) => {
                notify(error.body, 'error', 1000);
                this.securityMessage = error.body;
            });
        }, (error) => {
            if (error.status === 401) {
                this.securityMessage = error._body;
                console.error('user-list:updateRecord (auth error)', error);
                setTimeout(() => {
                    console.log('redirecting');
                    this.globals.isAuthenticated = false;
                    this._router.navigate(['/Login']);
                }, 5000);
            }
        });
    }
    updateRecord(d) {
        // console.log('Saving Record(d)', d);
        const updRec: User = {
            Id: d.key.Id,
            UserName: d.newData.UserName === undefined ? d.oldData.UserName : d.newData.UserName,
            FirstName: d.newData.FirstName === undefined ? d.oldData.FirstName : d.newData.FirstName,
            LastName: d.newData.LastName === undefined ? d.oldData.LastName : d.newData.LastName,
            Email: d.newData.Email === undefined ? d.oldData.Email : d.newData.Email,
            PhoneNumber: d.newData.PhoneNumber === undefined ? d.oldData.PhoneNumber : d.newData.PhoneNumber,
            RoleId: d.newData.RoleId === undefined ? d.oldData.RoleId : d.newData.RoleId,
            UserLoginId: d.newData.UserLoginId === undefined ? d.oldData.UserLoginId : d.newData.UserLoginId,
            Created: d.newData.Created === undefined ? d.oldData.Created : d.newData.Created,
            CreatedBy: d.newData.CreatedBy === undefined ? d.oldData.CreatedBy : d.newData.CreatedBy,
            LastModified: new Date().toISOString(),
            LastModifiedBy: this.globals.user.userName,
            Archived: d.newData.Archived === undefined ? d.oldData.Archived : d.newData.Archived,
            UserLogin: {
                Id: d.newData.UserLogin.Id === undefined ? d.oldData.UserLogin.Id : d.newData.UserLogin.Id,
                IsAdmin: d.newData.UserLogin.IsAdmin === undefined ? d.oldData.UserLogin.IsAdmin : d.newData.UserLogin.IsAdmin,
                AccessFailedCount: d.newData.UserLogin.AccessFailedCount === undefined ? d.oldData.UserLogin.AccessFailedCount
                    : d.newData.UserLogin.AccessFailedCount,
                Created: d.newData.UserLogin.Created === undefined ? d.oldData.UserLogin.Created : d.newData.UserLogin.Created,
                CreatedBy: d.newData.UserLogin.CreatedBy === undefined ? d.oldData.UserLogin.CreatedBy : d.newData.UserLogin.CreatedBy,
                LastModified: new Date().toISOString(),
                LastModifiedBy: this.globals.user.userName,
                LockoutEnabled: d.newData.UserLogin.LockoutEnabled === undefined ? d.oldData.UserLogin.LockoutEnabled
                    : d.newData.UserLogin.LockoutEnabled,
                LockoutEnd: new Date('1/1/1900').toISOString(),
                Login: d.newData.UserLogin.Login === undefined ? d.oldData.UserLogin.Login : d.newData.UserLogin.Login,
                PasswordHash: d.newData.UserLogin.PasswordHash === undefined ? d.oldData.UserLogin.IsAdPasswordHashmin
                    : d.newData.UserLogin.PasswordHash,
                RefreshId: d.newData.UserLogin.RefreshId === undefined ? d.oldData.UserLogin.RefreshId : d.newData.UserLogin.RefreshId,
            },
            Role: this.roleList.filter(p => p.Id === updRec.RoleId).length > 0 ? this.roleList.filter(p => p.Id === updRec.RoleId)[0] : null
        };

        this.userService.saveUser(this.globals.user.userName, updRec).subscribe(res => {
        }, (error) => {
            if (error.status === 401) {
                this.securityMessage = error._body;
                console.error('user-list:updateRecord (auth error)', error);
                setTimeout(() => {
                    console.log('redirecting');
                    this.globals.isAuthenticated = false;
                    this._router.navigate(['/Login']);
                }, 5000);
            }
        });
    }
    ngOnInit() {
    }

}
