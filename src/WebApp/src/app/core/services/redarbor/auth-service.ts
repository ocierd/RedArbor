import { inject, Injectable, signal, Signal, WritableSignal } from '@angular/core';
import { LoginData } from '@models/login.model';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { AuthToken } from '@models/auth.model';
import { StorageService } from '../../../shared/services/storage-service';
import { RedarborBaseService } from '@services/bases/redarbor-base-service';




/**
 * Redarbor authentication service
 */
@Injectable({
  providedIn: 'root',
})
export class AuthService extends RedarborBaseService {

  /**
   * Storage service to manage local storage
   */
  private readonly storageService: StorageService = inject(StorageService);


  /**
   * Subject to notify when the session should be initialized. This can be used to trigger actions when the user logs in or when the token is refreshed.
   */
  private static initSession: BehaviorSubject<boolean>;


  get sessionInit$(): Observable<boolean> {
    return AuthService.initSession.asObservable();
  }

  /**
   * Auth service constructor
   */
  constructor() {
    super('Auth');
    if (!AuthService.initSession) {
      AuthService.initSession = new BehaviorSubject<boolean>(false);
    }
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
    AuthService.initSession.next(true);
    this.storageService
      .setItem('auth_token', JSON.stringify(token));
  }


  /**
 * Get token from local storage
   * @returns Get Local storage token
   */
  getLocalStorageToken(): AuthToken | null {
    const token = this.storageService
      .getItem('auth_token');
    if (token) {
      return JSON.parse(token) as AuthToken;
    }
    return null;
  }

  /**
   * Check if user is athenticated
   * @returns True if the is user is authenticated; false otherwise
   */
  isAuthenticated(): boolean {
    // Implement your logic to check if the user is authenticated
    const token = this.getLocalStorageToken();
    if (token && token.token) {
      return true;
    }
    return false;
  }

  /**
   * Close user session by removing token from local storage.
   * Clear all session data and redirect to login page if necessary.
   * This method can be called when the user logs out or when the token expires.
   */
  closeSession(): void {
    this.storageService.clear();
    AuthService.initSession.next(false);
    AuthService.initSession.complete();
  }




}

@Injectable({
  providedIn: 'root',
})
export class ExpirationTokenState {

  private readonly authService = inject(AuthService);

  private static _expiration: WritableSignal<number> ;

  private interval: any;


  public get expiration(): Signal<number> {
    return ExpirationTokenState._expiration.asReadonly();
  }

  constructor() {
    ExpirationTokenState._expiration = signal(this.getLeftTime());
    this.startInterval();
  }

  startInterval(): void {
    const exp = ExpirationTokenState._expiration();
    if (exp > 0) {
      this.initInterval();
    }
  }

  private initInterval(): void {
    if (this.interval) {
      clearInterval(this.interval);
    }

    this.interval = setInterval(() => {
      const exp = ExpirationTokenState._expiration();
      const newExp = exp - 1;

      if (exp > 0) {
        ExpirationTokenState._expiration.set(newExp);
      }

      if (newExp <= 0) {
        clearInterval(this.interval);
      }
    }, 1000);
  }


  private getLeftTime(): number {
    const authToken = this.authService.getLocalStorageToken();
    if (!authToken) {
      console.log('Regresó -1');

      return -1;
    }
    authToken.createdAt = new Date(authToken.createdAt);
    const currentTime = new Date();
    const elapsedSeconds = Math.floor((currentTime.getTime() - authToken.createdAt.getTime()) / 1000);
    const leftTime = authToken.expiresIn * 60 - elapsedSeconds;
    console.log('Left TIME: ', leftTime);

    return leftTime > 0 ? leftTime : -1;
  }


}