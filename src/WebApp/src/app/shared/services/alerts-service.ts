import { inject, Injectable } from '@angular/core';
import { DialogService } from './dialog-service';
import { SimpleDialog } from '../components/dialogs/simple-dialog/simple-dialog';
import { MatDialogRef } from '@angular/material/dialog';
import { firstValueFrom } from 'rxjs';
import { AlertDialogData, ConfirmDialogData } from '@models/gui/alert-dialog-data';

/**
 * Service to manage alert messages across the app, using a simple dialog component to display them.
 * Provides methods to send both simple alerts and confirmation dialogs, with customizable messages and titles.
 */
@Injectable({
  providedIn: 'root',
})
export class AlertsService {

  /**
   * Default title for alerts
   */
  private readonly ALERT_DEFAULT_TITLE: string = 'Alerta';

  /**
   * Dialog service to open dialogs from anywhere in the app
   */
  private readonly dialogService: DialogService = inject(DialogService);


  async sendAlertMessageAsync(message: string): Promise<void> {
    const data: AlertDialogData = {
      title: this.ALERT_DEFAULT_TITLE,
      message: message
    };
    await this.sendAlertAsync(data);
  }

  async sendConfirmAlertAsync(message: string): Promise<boolean> {
    const data: ConfirmDialogData = {
      title: this.ALERT_DEFAULT_TITLE,
      message: message,
      confirmText: 'Sí',
      cancelText: 'No',
      showCancelButton: true,
    };
    const confirmed = await this.sendAlertAsync(data);
    return confirmed;
  }

  async sendAlertAsync(data: ConfirmDialogData | AlertDialogData): Promise<boolean> {
    const afterClosed = this.sendAlert(data).afterClosed();
    const confirmed = await firstValueFrom(afterClosed);
    if (!confirmed) {
      return false;
    }
    return confirmed;
  }




  sendAlert(data: ConfirmDialogData | AlertDialogData): MatDialogRef<SimpleDialog, boolean> {
    return this.dialogService.openDialog(SimpleDialog, {
      title: data.title ?? this.ALERT_DEFAULT_TITLE,
      message: data.message
    });
  }

}
