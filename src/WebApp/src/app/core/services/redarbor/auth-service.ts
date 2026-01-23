import { Injectable } from '@angular/core';
import { RedarborBaseService } from './redarbor-base-service';
import { LoginData } from '@models/login.model';
import { Observable } from 'rxjs';
import { AuthToken } from '@models/auth.model';
import { StorageService } from '../../../shared/services/storage-service';


/**
 * Redarbor authentication service
 */
@Injectable({
  providedIn: 'root',
})
export class AuthService extends RedarborBaseService {

  storageService: StorageService;

  /**
   * Auth service constructor
   */
  constructor(storageService: StorageService) {
    super('Auth');
    this.storageService = storageService;
  }

  /**
   * Authenticate user
   * @param login Login data
   * @returns Response of authenticate
   */
  auth(login: LoginData): Observable<AuthToken> {
    return this.post('/Authenticate', login);
  }


  /**
   * Save token in local storage
   * @param token Auth token to be saved
   */
  setLocalStorageToken(token: AuthToken): void {
    this.storageService
      .setItem('auth_token', JSON.stringify(token));
  }


  /**
 * Get token from local storage
   * @returns Get Local storage token
   */
  getLocalStorageToken(): string | null {
    return this.storageService
      .getItem('auth_token');
  }

  /**
   * Check if user is athenticated
   * @returns True if the is user is authenticated; false otherwise
   */
  isAuthenticated(): boolean {
    // Implement your logic to check if the user is authenticated
    const token = this.getLocalStorageToken();
    if (token) {
      const jwt = JSON.parse(token) as AuthToken;
      if (jwt.token) {
        return true;
      }
      return false;
    }
    return false;
  }

}
