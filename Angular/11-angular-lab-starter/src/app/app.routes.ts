import { Routes } from '@angular/router';

import { authGuard } from './guards/auth.guard';
import { AboutUsComponent } from './pages/about-us/about-us.component';
import { ContactUsComponent } from './pages/contact-us/contact-us.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ShopComponent } from './pages/shop/shop.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, title: 'Home · ShopEase' },
  { path: 'shop', component: ShopComponent, title: 'Shop · ShopEase' },
  { path: 'about', component: AboutUsComponent, title: 'About Us · ShopEase' },
  { path: 'contact', component: ContactUsComponent, title: 'Contact Us · ShopEase' },
  { path: 'login', component: LoginComponent, title: 'Sign in · ShopEase' },

  // The whole admin area is guarded once, here, and lazy-loaded as one chunk.
  {
    path: 'dashboard',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./pages/dashboard/dashboard.routes').then((m) => m.DASHBOARD_ROUTES),
  },

  { path: '**', redirectTo: '' },
];
