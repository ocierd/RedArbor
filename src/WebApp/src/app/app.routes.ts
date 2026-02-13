import { Routes } from '@angular/router';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { MainLayout } from './layouts/main-layout/main-layout';
import { authGuard } from './core/guards/auth-guard';

/**
 * Application routes configuration
 * Defines the main routing structure of the application
 */
export const routes: Routes = [
    { path: '', redirectTo: 'redarbor', pathMatch: 'full' },
    {
        path: 'auth', component: AuthLayout,
        loadChildren: () => import('@modules/auth/auth-module')
            .then(m => m.AuthModule)
    },
    {
        path: 'redarbor', component: MainLayout,
        canActivate: [authGuard],
        canActivateChild: [authGuard],
        loadChildren: () => import('@modules/redarbor/redarbor-module')
            .then(m => m.RedarborModule)
    }
];
