import { UserGroup } from 'src/app/domain/UserGroup';

export class Permission {
    ID?: number;
    userGroup?: UserGroup;
    PageId?: number;
    CanView?: boolean;
    CanAdd?: boolean;
    CanEdit?: boolean;
    CanDelete?: boolean;
}