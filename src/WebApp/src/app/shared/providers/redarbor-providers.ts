
import { Provider } from '@angular/core';
import { DatePipe } from '@angular/common';
import { TimePipe } from '../pipes/time-pipe';


const pipesProviders: Provider[] = [
    { provide: DatePipe, useClass: DatePipe },
    { provide: TimePipe, useClass: TimePipe }
];


export const RedarborProviders: Provider[] = [
    DatePipe, TimePipe
    
    // ...pipesProviders
];