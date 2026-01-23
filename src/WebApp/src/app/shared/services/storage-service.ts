import { Injectable } from '@angular/core';


/**
 * Storage service for managing local and session storage
 */
@Injectable({
  providedIn: 'root',
})
export class StorageService {

  sessionStorage: Storage = sessionStorage;
  localStorage: Storage = localStorage;

  /**
   * Set item in storage
   * @param key Key of item
   * @param value Value to save in storage
   * @param useSession Indicates if use "SessionStorage" if ture or "LocalStorage" for false
   */
  setItem(key: string, value: string, useSession: boolean = true): void {
    if (useSession) {
      this.sessionStorage.setItem(key, value);
    } else {
      this.localStorage.setItem(key, value);
    }
  }


  getItem(key: string, useSession: boolean = true): string | null {
    if (useSession) {
      return this.sessionStorage.getItem(key);

    } else {
      return this.localStorage.getItem(key);
    }
  }

  getItemAs<T>(key: string, useSession: boolean = true): T | null {
    const item = this.getItem(key, useSession);
    return item ? JSON.parse(item) as T : null;
  }

  setObject<T>(key: string, value: T, useSession: boolean = true): void {
    const jsonString = JSON.stringify(value);
    this.setItem(key, jsonString, useSession);
  }
}
