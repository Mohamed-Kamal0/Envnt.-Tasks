import { Routes } from "@angular/router";
import { DashboardComponent } from "./dashboard.component";
import { ProductListComponent } from "./product-list/product-list.component";
import { ProductDetailComponent } from "./product-detail/product-detail.component";
import { ProductEditComponent } from "./product-edit/product-edit.component";

/**
 * Nested routes rendered inside DashboardComponent's own <router-outlet>.
 * `authGuard` on the parent /dashboard route in app.routes.ts already protects
 * everything underneath it.
 */
export const DASHBOARD_ROUTES: Routes = [
  { path: "", component: DashboardComponent, title: "Dashboard · ShopEase" },
  {
    path: "products",
    component: ProductListComponent,
    title: "Product List · ShopEase",
  },
  {
    path: "products/:id",
    component: ProductDetailComponent,
    title: "Product Detail · ShopEase",
  },
  {
    path: "products/:id/edit",
    component: ProductEditComponent,
    title: "Product Edit · ShopEase",
  },
  { path: "**", component: DashboardComponent, title: "Dashboard · ShopEase" },
];
