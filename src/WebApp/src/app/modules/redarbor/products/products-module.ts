import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing-module';
import { ProductsList } from './pages/products-list/products-list';
import { FormField } from "@angular/forms/signals";
import { SharedModule } from '../../../shared/shared-module';


@NgModule({
  declarations: [
    ProductsList
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    SharedModule,
    FormField
]
})
export class ProductsModule { }
