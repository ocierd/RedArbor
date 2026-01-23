import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@services/redarbor/auth-service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.html',
  styleUrl: './login.scss',
  standalone: false
})
export class Login {

  fb: FormBuilder = inject(FormBuilder);
  authService: AuthService = inject(AuthService);

  usersLoginData = [
    { username: 'admin', password: 'Password123!', roles: ['Administrator', 'InventoryManager'] },
    { username: 'user', password: 'Password123!', roles: ['User'] },
    { username: 'inventoryManager', password: 'Password123!', roles: ['InventoryManager'] }
  ]

  loginForm: FormGroup = this.fb.group({
    username: ['admin', Validators.required],
    password: ['Password123!', Validators.required]
  });

  router: Router = inject(Router);


  async onSubmit() {

    try {
      const authToken = await firstValueFrom(this.authService.auth(this.loginForm.value));
      this.authService.setLocalStorageToken(authToken);
      console.log("Login successful, token stored: ", authToken);
      this.router.navigate(['/redarbor'],{ });
    } catch (error) {
      console.error(error);

    }
    finally {
      console.log("Login attempt finished.");
    }

  }

}
