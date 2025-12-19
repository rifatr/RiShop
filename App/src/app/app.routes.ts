import { Routes } from '@angular/router';
import { Home } from './features/home/home';

export const routes: Routes = [
    {path: '', component: Home, data: {breadcrumb: 'Home' }},
    {path: 'shop', loadChildren: () => import('./features/shop/shop.routes').then(m => m.ShopRoutes)},
    {path: 'cart', loadChildren: () => import('./features/cart/cart.routes').then(m => m.CartRoutes)},
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
