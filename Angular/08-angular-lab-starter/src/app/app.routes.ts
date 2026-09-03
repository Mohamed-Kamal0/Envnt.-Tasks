import { Routes } from "@angular/router";
import { HomeComponent } from "./pages/home/home.component";
import { AboutUsComponent } from "./pages/about-us/about-us.component";
import { ShopComponent } from "./pages/shop/shop.component";
import { ContactUsComponent } from "./pages/contact-us/contact-us.component";
import { NotfoundComponent } from "./components/notfound/notfound.component";

// #Task 2: Build the Routes array with one entry per page — Home at the
// root path, plus Shop, About, and Contact — each pointing at its matching
// imported component and given a descriptive page title. Add a final
// wildcard entry that catches any unmatched URL and redirects back to
// the home page.

export const routes: Routes = [

  { path: "home", component: HomeComponent },
  { path: "about", component: AboutUsComponent },
  { path: "shop", component: ShopComponent },
  { path: "contact", component: ContactUsComponent },
  { path: "**", component: NotfoundComponent },
];
