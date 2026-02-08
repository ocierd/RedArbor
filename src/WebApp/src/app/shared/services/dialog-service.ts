import { inject, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/**
 * Dialog service to open dialogs from anywhere in the app
 */
@Injectable({
    providedIn: 'root',
})
export class DialogService {

    dialog = inject(MatDialog);

    /**
     * Open a dialog with the specified component and data
     * @param component Component to open
     * @param data Data to display in the the dialog
     * @returns Dialog reference
     */
    openDialog(component: any, data?: any) {
        return this.dialog.open(component, {
            data: data,
        });
    }

}
