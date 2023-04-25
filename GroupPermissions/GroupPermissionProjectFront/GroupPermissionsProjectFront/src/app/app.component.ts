import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { UserGroup } from './domain/UserGroup';
import { Permission } from './domain/Permission';

import { UserGroupService } from './services/UserGroupservice';


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [ConfirmationService, MessageService, UserGroupService]
})
export class AppComponent implements OnInit {

    userGroupDialog: boolean;
    // products: UserGroup[];
    userGroup: UserGroup;
    submitted: boolean;
    userGroupsList: UserGroup[];
    userGroupID: Number;
    Permissions :Permission[];
    Permission :Permission;
    selectedPermission: Permission[];

    constructor(private UserGroupService: UserGroupService, private messageService: MessageService, private confirmationService: ConfirmationService) { }

    ngOnInit() {
        //this.userGroupsList =null;
        this.UserGroupService.GetAllUserGroups().subscribe(
            (result: any) => {
                this.userGroupsList = result.data;
                debugger
                // for (let i = 0; i < result.data.length; i++) {
                //     debugger
                //     let group = result.data[i] as any;
                //     debugger
                //     this.userGroupsList.push(group);
                //     debugger
                // }
            },
            error => {
                console.log(error);
            }
        )
        this.UserGroupService.GetPermissionsByGroupId(0).then(data => this.Permissions = data);
    };

    openNew() {
        this.userGroup = {};
        this.submitted = false;
        this.userGroupDialog = true;
    };

    check(obj) {
        debugger
        this.UserGroupService.GetPermissionsByGroupId(obj.ID).then(data => this.Permissions = data);
    }


    // deleteSelectedProducts() {
    //     this.confirmationService.confirm({
    //         message: 'Are you sure you want to delete the selected products?',
    //         header: 'Confirm',
    //         icon: 'pi pi-exclamation-triangle',
    //         accept: () => {
    //             this.products = this.products.filter(val => !this.selectedProducts.includes(val));
    //             this.selectedProducts = null;
    //             this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Products Deleted', life: 3000 });
    //         }
    //     });
    // }

    // editProduct(product: Product) {
    //     this.product = { ...product };
    //     this.userGroupDialog = true;
    // }

    // deleteProduct(product: Product) {
    //     this.confirmationService.confirm({
    //         message: 'Are you sure you want to delete ' + product.name + '?',
    //         header: 'Confirm',
    //         icon: 'pi pi-exclamation-triangle',
    //         accept: () => {
    //             this.products = this.products.filter(val => val.id !== product.id);
    //             this.product = {};
    //             this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Deleted', life: 3000 });
    //         }
    //     });
    // }

    hideDialog() {
        this.userGroupDialog = false;
        this.submitted = false;
    }

    saveUserGroup() {
        this.submitted = true;
        if (this.userGroup.ArabicName.trim()) {
            if (this.userGroup.id) {
                this.UserGroupService.UpdateGroupCategory(this.userGroup).subscribe(
                    (result: any) => {

                        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'UserGroup Updated', life: 3000 });
                    },
                    error => {
                        this.messageService.add({ severity: 'error', summary: 'fail', detail: 'UserGroup Not Updated', life: 3000 });
                    }
                );
            }
            else {
                this.UserGroupService.AddNewGroupCategory(this.userGroup).subscribe(
                    (result: any) => {
                        debugger
                        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'UserGroup Created', life: 3000 });
                    },
                    error => {
                        debugger
                        this.messageService.add({ severity: 'error', summary: 'fail', detail: 'UserGroup Not Created', life: 3000 });
                    }
                );
                debugger
            }
            this.userGroupDialog = false;
            this.userGroup = {};
        }
    }

    // findIndexById(id: string): number {
    //     let index = -1;
    //     for (let i = 0; i < this.products.length; i++) {
    //         if (this.products[i].id === id) {
    //             index = i;
    //             break;
    //         }
    //     }

    //     return index;
    // }

    createId(): string {
        let id = '';
        var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        for (var i = 0; i < 5; i++) {
            id += chars.charAt(Math.floor(Math.random() * chars.length));
        }
        return id;
    }
}
