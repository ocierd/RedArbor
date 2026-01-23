import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainHome } from './pages/main-home/main-home';

const routes: Routes = [
  { path: '', redirectTo: 'main',pathMatch:'full' },
  { path: 'main', component: MainHome }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
