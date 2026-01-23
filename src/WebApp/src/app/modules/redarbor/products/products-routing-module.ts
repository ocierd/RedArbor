import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsList } from './pages/products-list/products-list';

const routes: Routes = [
  { path: '', redirectTo:'products-list', pathMatch:'full' },
  { path: 'products-list', component: ProductsList }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
