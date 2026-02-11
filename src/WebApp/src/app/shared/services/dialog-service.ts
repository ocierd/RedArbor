import { ComponentType } from '@angular/cdk/overlay';
import { inject, Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

/**
 * Dialog service to open dialogs from anywhere in the app
 */
@Injectable({
    providedIn: 'root',
})
export class DialogService {

    matDialog = inject(MatDialog);

    /**
     * Open a dialog with the specified component and data
     * @param component Component to open
     * @param data Data to display in the the dialog
     * @returns Dialog reference
     */
    openDialog<T, D = any, R = any>(component: ComponentType<T>, data?: D): MatDialogRef<T, R> {
        return this.matDialog.open(component, {
            data: data,
        });
    }

}
