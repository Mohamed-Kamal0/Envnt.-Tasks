import { Routes } from "@angular/router";

import { authGuard } from "./guards/auth.guard";
import { AboutUsComponent } from "./pages/about-us/about-us.component";
import { ContactUsComponent } from "./pages/contact-us/contact-us.component";
import { DashboardComponent } from "./pages/dashboard/dashboard.component";
import { HomeComponent } from "./pages/home/home.component";
import { LoginComponent } from "./pages/login/login.component";
import { ShopComponent } from "./pages/shop/shop.component";
import { logInGuard } from "./guards/login.guard";

export const routes: Routes = [
  { path: "", component: HomeComponent, title: "Home · ShopEase" },
  { path: "shop", component: ShopComponent, title: "Shop · ShopEase" },
  { path: "about", component: AboutUsComponent, title: "About Us · ShopEase" },
  {
    path: "contact",
    component: ContactUsComponent,
    title: "Contact Us · ShopEase",
  },

  // DAY 4, STEP 1
  {
    path: "login",
    component: LoginComponent,
    title: "Sign in · ShopEase",
    canActivate: [logInGuard],
  },

  // DAY 4, STEP 5 — canActivate attaches the guard built above. authGuard
  // currently always returns true (see guards/auth.guard.ts), so this route
  // is structurally wired but not yet actually protected.
  {
    path: "dashboard",
    component: DashboardComponent,
    canActivate: [authGuard], // Becomes a real login requirement once #Task 5 (the guard's authentication check) is implemented
    title: "Dashboard · ShopEase",
  },

  { path: "**", redirectTo: "" },
];
