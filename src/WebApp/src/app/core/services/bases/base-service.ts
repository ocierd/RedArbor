import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Inject, InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';

export const API_URL_TOKEN = new InjectionToken<string>('API_URL');

/**
 * Base service class for API requests
 */
@Injectable({
  providedIn: 'root',
})
export class BaseService {

  /**
   * API url with controller name
   */
  private API_URL: string;

  /**
   * Http client to perform requests
   */
  private httpClient: HttpClient = inject(HttpClient);

  /**
   * Base service constructor
   * @param apiUrl Api url with controller name
   */
  constructor(@Inject(API_URL_TOKEN) apiUrl: string) {
    this.API_URL = apiUrl;
  }



  /**
   * Send Get request to API
   * @param url Full or partial request
   * @returns Observable ot T
   */
  get<T>(url: string): Observable<T> {
    return this.sendRequest('GET', url);
  }


  /**
   * Send Post request to API
   * @param url Partial or full request
   * @param body Body to send
   * @returns Observable of response
   */
  post<T>(url: string, body: any): Observable<T> {
    return this.sendRequest('POST', url, body);
  }


  /**
   * Send request to API
   * @param method Method of request (Also knowns as VERB)
   * @param url Partial or full URL request
   * @param body Body to send in the request
   * @returns Observable of response
   */
  sendRequest<T>(method: string, url: string, body?: any)
    : Observable<T> {
    const sendReqObs = this.httpClient
      .request(method, `${this.API_URL}${url}`,
        { body: body });
    return sendReqObs as Observable<T>;
  }



}
