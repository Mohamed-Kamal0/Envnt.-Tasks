import { Component } from '@angular/core';

import { HeroComponent } from '../../components/hero/hero.component';
import { ProductsComponent } from '../../components/products/products.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeroComponent, ProductsComponent],
  templateUrl: './home.component.html',
})
export class HomeComponent {}
