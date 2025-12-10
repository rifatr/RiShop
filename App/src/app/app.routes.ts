import { Routes } from '@angular/router';
import { Home } from './home/home';

export const routes: Routes = [
    {path: '', component: Home, data: {breadcrumb: 'Home' }},
    {path: 'shop', loadChildren: () => import('./features/shop/shop.routes').then(m => m.SHOP_ROUTES)},
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
