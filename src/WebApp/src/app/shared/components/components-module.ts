import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Error } from './generic/error/error';
import { MaterialModule } from '../material/material-module';
import { SimpleDialog } from './dialogs/simple-dialog/simple-dialog';
import { Grid } from './generic/grid/grid';
import { SpinnerButton } from './generic/spinner-button/spinner-button';
import { Spinner } from './generic/spinner/spinner';
import { PipesModule } from '../pipes/pipes-module';

/**
 * Module that declares and exports all shared components used across the app
 */
const components = [
    Error,
    SimpleDialog,
    Grid,
    SpinnerButton,
    Spinner,
];


@NgModule({
  declarations: [
    ...components,
    
  ],
  imports: [
    CommonModule,
    MaterialModule,
    PipesModule
],
  exports: [
    ...components
  ],
})
export class ComponentsModule { }
