import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Error } from './generic/error/error';
import { MaterialModule } from '../material/material-module';


const components = [
    Error
];


@NgModule({
  declarations: [
    ...components
  ],
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports: [
    ...components
  ],
})
export class ComponentsModule { }
