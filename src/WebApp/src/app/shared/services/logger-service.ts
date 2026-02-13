import { Injectable } from '@angular/core';

/**
 * LoggerService is a simple wrapper around the native console object, providing methods for logging messages, errors, warnings, and informational messages. This service can be injected into any component or service in the Angular application to facilitate consistent logging practices and allow for potential future enhancements such as log levels or remote logging.
 */
@Injectable({
  providedIn: 'root',
})
export class LoggerService {

  private static _console: Console = console;

  /**
   * This service provides a wrapper around the native console object, allowing for potential future enhancements such as log levels, remote logging, or formatting. For now, it simply exposes the standard console methods (log, error, warn, info) through getters.
   */
  get log(){
    return LoggerService._console.log;
  }

  /**
   * Getter for the console.error method, allowing for error logging through this service.
   */
  get error(){
    return LoggerService._console.error;
  }

  /**
   * Getter for the console.warn method, allowing for warning logging through this service.
   */
  get warn(){
    return LoggerService._console.warn;
  }

  /**
   * Getter for the console.info method, allowing for informational logging through this service.
   */
  get info(){
    return LoggerService._console.info;
  } 



}
