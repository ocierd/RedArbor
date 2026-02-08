import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { inject } from '@angular/core';
import { AuthService } from '@services/redarbor/auth-service';

/**
 * Intercepts HTTP requests to add an Authorization header if the request URL matches the API URL.
 * @param req Request to handle
 * @param next Handler function
 * @returns Interceptor function
 */
export const multiServerInterceptor: HttpInterceptorFn = (req, next) => {

  if (req.url.includes(environment.apiUrl)) {
    console.log('PASÓ POR INTERCEPTOR');
    
    const token = inject(AuthService).getLocalStorageToken();
    if (!token) {
      return next(req);
    }

    const modifiedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token.token}`,
      },
    });
    return next(modifiedReq);
  }

  return next(req);
};
