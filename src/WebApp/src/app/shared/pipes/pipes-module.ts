import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { DynamicPipe } from './dynamic-pipe';


/**
 * Module that declares and exports all shared pipes used across the app
 */
const pipes = [
  DynamicPipe,
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
