import { Component } from "@angular/core";
import { HeroComponent } from "../../components/hero/hero.component";
import { NavbarComponent } from "../../components/navbar/navbar.component";
import { FooterComponent } from "../../components/footer/footer.component";
import { ContactUsComponent } from "../contact-us/contact-us.component";
import { AboutUsComponent } from "../about-us/about-us.component";
import { ShopComponent } from "../shop/shop.component";

/**
 * #Task 4: Compose the four components together to form the landing
 * page, in this order — navbar, hero banner, products area, then footer.
 * You'll need to import NavbarComponent, HeroComponent, ProductsComponent,
 * and FooterComponent into this component's `imports` array, then place
 * them in the template.
 */
@Component({
  selector: "app-home",
  standalone: true,
  imports: [HeroComponent, ShopComponent, AboutUsComponent, ContactUsComponent],
  templateUrl: "./home.component.html",
  styleUrl: "./home.component.css",
})
export class HomeComponent {}
