import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing-module';
import { ProductsList } from './pages/products-list/products-list';
import { MaterialModule } from "../../../shared/material/material-module";
import { FormField } from "@angular/forms/signals";


@NgModule({
  declarations: [
    ProductsList
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    MaterialModule,
    FormField
]
})
export class ProductsModule { }
