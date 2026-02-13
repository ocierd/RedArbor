import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Error } from './generic/error/error';
import { MaterialModule } from '../material/material-module';
import { SimpleDialog } from './dialogs/simple-dialog/simple-dialog';
import { Grid } from './generic/grid/grid';
import { SpinnerButton } from './generic/spinner-button/spinner-button';
import { Spinner } from './generic/spinner/spinner';

/**
 * Module that declares and exports all shared components used across the app
 */
const components = [
    Error,
    SimpleDialog,
    Grid,
    SpinnerButton,
];


@NgModule({
  declarations: [
    ...components,
    Spinner,
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
