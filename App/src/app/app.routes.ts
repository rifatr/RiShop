import { Routes } from '@angular/router';
import { Home } from './home/home';

export const routes: Routes = [
    {path: '', component: Home, data: {breadcrumb: 'Home' }},
    {path: 'shop', loadComponent: () => import('./features/shop/shop').then(m => m.Shop)},
    {path: 'shop/:id', loadComponent: () => import('./features/product-details/product-details').then(m => m.ProductDetails)},
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
