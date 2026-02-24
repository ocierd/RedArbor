import { Pipe, PipeTransform } from '@angular/core';
import { DateUtils } from '../utils/date-utils';

/**
 * Pipe to format time values in seconds to a human-readable format with appropriate time units
 * Usage: {{ timeValueInSeconds | time }}
 * Example: 3600 seconds will be formatted as "1 H"
 */
@Pipe({
  name: 'time',
  standalone: false
})
export class TimePipe implements PipeTransform {

  /**
   * Transform a time value in seconds to a human-readable format with appropriate time units
   * @param value Time value in seconds to format
   * @param args Optional arguments for the pipe (not used in this implementation)
   * @returns Formatted time string
   */
  transform(value: number, args?: string): unknown {
    if (value) {
      return DateUtils.formatTime(value);
    }
    return value;
  }

}
