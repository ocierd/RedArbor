import { Component, inject, signal, WritableSignal } from '@angular/core';
import { FieldTree, form, maxLength, minLength, required } from '@angular/forms/signals';
import { Router } from '@angular/router';
import { AuthService } from '@services/redarbor/auth-service';
import { firstValueFrom } from 'rxjs';
import { DialogService } from '../../../../shared/services/dialog-service';
import { AlertsService } from '../../../../shared/services/alerts-service';

type loginForm = { username: string, password: string };

@Component({
  selector: 'app-login',
  templateUrl: './login.html',
  styleUrl: './login.scss',
  standalone: false
})
export class Login {

  readonly authService: AuthService = inject(AuthService);
  readonly router: Router = inject(Router);
  readonly dialogService = inject(DialogService);
  readonly alertsService = inject(AlertsService);


  usersLoginData = [
    { username: 'admin', password: 'Password123!', roles: ['Administrator', 'InventoryManager'] },
    { username: 'user', password: 'Password123!', roles: ['User'] },
    { username: 'inventoryManager', password: 'Password123!', roles: ['InventoryManager'] }
  ]

  loginData: WritableSignal<loginForm> = signal({ username: 'admin', password: 'Password123!' });

  loginForm: FieldTree<loginForm> = form(this.loginData,
    opts => {
      required(opts.username);
      required(opts.password);
      minLength(opts.username, 3);
      maxLength(opts.username, 5);

    });


  /**
   * Handle form submission
   */
  async onSubmit() {
    try {
      const data = this.loginForm().value();
      const authToken = await firstValueFrom(this.authService.auth(data));
      this.authService.setLocalStorageToken(authToken);
      this.router.navigate(['/redarbor'], {});
    } catch (error) {
      this.alertsService
        .sendAlertMessageAsync("Ocurrió un error al iniciar sesión");
    }
    finally {
      console.log("Login attempt finished.");
    }

  }

}
