import { inject } from '@angular/core';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { AuthService } from '@services/redarbor/auth-service';

/**
 * Authentication guard to protect routes
 * @param route Activated route snapshot
 * @param state Router state snapshot
 * @returns 
 */
export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  console.log("Url actual: ", route.url);


  if (authService.isAuthenticated()) {
    console.log('Is authenticated');
    
    return true;
  }
  const router = inject(Router);

  const loginPath = router.parseUrl("/auth");
  return loginPath;
};
