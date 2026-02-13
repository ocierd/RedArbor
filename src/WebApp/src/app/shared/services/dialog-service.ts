import { ComponentType } from '@angular/cdk/overlay';
import { inject, Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';

/**
 * Dialog service to open dialogs from anywhere in the app
 */
@Injectable({
    providedIn: 'root',
})
export class DialogService {

    matDialog = inject(MatDialog);

    /**
     * Open a dialog with the specified component and configuration
     * @param component Component to open
     * @param config Configuration for the dialog
     * @returns Dialog reference
     */
    openDialog<T, D = any, R = any>(component: ComponentType<T>, config?: MatDialogConfig<D>): MatDialogRef<T, R> {
        return this.matDialog.open(component, config);
    }

}
