import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

/**
 * Redarbor module routes
 */
const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    loadChildren: () => import('@modules/redarbor/home/home-module')
    .then(m => m.HomeModule)
  },
  { path: 'products', 
    loadChildren: () => import('./products/products-module')
    .then(m => m.ProductsModule) }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RedarborRoutingModule { }
