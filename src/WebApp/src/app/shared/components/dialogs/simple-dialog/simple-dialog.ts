import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AlertDialogData, ConfirmDialogData } from '@models/gui/alert-dialog-data';

@Component({
  selector: 'app-simple-dialog',
  standalone: false,
  templateUrl: './simple-dialog.html',
  styleUrl: './simple-dialog.scss',
})
export class SimpleDialog {


  constructor(
    @Inject(MAT_DIALOG_DATA) public data: AlertDialogData | ConfirmDialogData,
    private dialogRef: MatDialogRef<SimpleDialog>) {

  }

  /**
   * Type guard to determine if the injected data is of type ConfirmDialogData
   * @param data Injected data from service
   * @returns True if is ConfirmDialogData
   */
  isConfirmDialog(data: AlertDialogData | ConfirmDialogData): data is ConfirmDialogData {
    return 'confirmText' in this.data || 'cancelText' in this.data || 'showCancelButton' in this.data;
  }


  /**
   * Closes the dialog with a positive confirmation
   */
  onConfirm(): void {
    this.dialogRef.close(true);
  }

  /**
   * Closes the dialog with a negative confirmation
   */
  onCancel(): void {
    this.dialogRef.close(false);
  }

}
