import { Routes } from '@angular/router';

import { AboutUsComponent } from './pages/about-us/about-us.component';
import { ContactUsComponent } from './pages/contact-us/contact-us.component';
import { HomeComponent } from './pages/home/home.component';
import { ShopComponent } from './pages/shop/shop.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, title: 'Home · ShopEase' },
  { path: 'shop', component: ShopComponent, title: 'Shop · ShopEase' },
  { path: 'about', component: AboutUsComponent, title: 'About Us · ShopEase' },
  { path: 'contact', component: ContactUsComponent, title: 'Contact Us · ShopEase' },
  { path: '**', redirectTo: '' },
];
