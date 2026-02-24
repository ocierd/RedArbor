import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { DynamicPipe } from './dynamic-pipe';
import { TimePipe } from './time-pipe';


/**
 * Module that declares and exports all shared pipes used across the app
 */
const pipes = [
  DynamicPipe,
  TimePipe
];

@NgModule({
  declarations: [
    ...pipes
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ...pipes
  ]
})
export class PipesModule { }
