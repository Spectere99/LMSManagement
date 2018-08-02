    import { Injectable } from '@angular/core';

@Injectable()
export class Globals {
    isAuthenticated = false;
    isAdmin = false;
    user = {
        userId: 0,
        userName: '',
        userFullName: '',
        userRole: '',
    };
}
