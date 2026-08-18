import { Component } from '@angular/core';

import { HeroComponent } from '../../components/hero/hero.component';
import { ProductsComponent } from '../../components/products/products.component';

/** Navbar and footer now live in AppComponent, so Home only needs Hero + Products. */
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeroComponent, ProductsComponent],
  templateUrl: './home.component.html',
})
export class HomeComponent {}
