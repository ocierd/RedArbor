type Alert_Icon = 'info' | 'warning' | 'error' | 'success';

/**
 * Data structure for alert dialogs, containing a title, message, and an optional icon to indicate the type of alert.
 */
export interface AlertDialogData {
    title: string;
    message: string;
    icon?: Alert_Icon;
}

/**
 * Data structure for confirmation dialogs, extending AlertDialogData with additional properties for confirmation and cancellation options.
 */
export interface ConfirmDialogData extends AlertDialogData {
    confirmText?: string;
    cancelText?: string;
    showCancelButton?: boolean;
}

