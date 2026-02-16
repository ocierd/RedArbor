import { inject } from '@angular/core';
import { CanActivateChildFn, CanActivateFn, Router } from '@angular/router';
import { AuthService } from '@services/redarbor/auth-service';
import { LoggerService } from '@shared-services/logger-service';

/**
 * Authentication guard to protect routes
 * @param route Activated route snapshot
 * @param state Router state snapshot
 * @returns 
 */
export const authGuard: CanActivateFn | CanActivateChildFn = (route, state) => {
  const logger = inject(LoggerService);
  const authService = inject(AuthService);
  
  if (authService.isAuthenticated()) {
    return true;
  }
  logger.warn('Not authenticated, redirecting to login');
  const router = inject(Router);
  const loginPath = router.parseUrl("/auth");
  return loginPath;
};
