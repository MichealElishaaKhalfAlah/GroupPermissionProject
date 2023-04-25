import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserGroup } from '../domain/UserGroup';
import { Observable } from 'rxjs';
import { Permission } from '../domain/Permission';


@Injectable({
    providedIn: 'root'
})
export class UserGroupService {

    baseUrl: string = "https://localhost:44356/"
    constructor(private http: HttpClient) { }

    GetAllUserGroups(): Observable<any> {
        return this.http.get(this.baseUrl + "API/UserGroup/GetAllUserGroups");
    };

    AddNewGroupCategory(obj: any): Observable<any> {
        return this.http.post(this.baseUrl + "API/UserGroup/AddUserGroup", obj);
    };

    UpdateGroupCategory(obj: any): Observable<any> {
        return this.http.post(this.baseUrl + "API/UserGroup/UpdateUserGroups", obj);
    };

    // GetPermissionsByGroupId(GroupId:number): Observable<any> {
    //     return this.http.get(this.baseUrl + "API/Permissions/GetPermissionsByGroupId?Groupid="+GroupId);
    // };

    GetPermissionsByGroupId(GroupId:number) {
        return this.http.get<any>(this.baseUrl + "API/Permissions/GetPermissionsByGroupId?Groupid="+GroupId)
        .toPromise()
        .then(res => <Permission[]>res.data)
        .then(data => { return data; });
    };
}