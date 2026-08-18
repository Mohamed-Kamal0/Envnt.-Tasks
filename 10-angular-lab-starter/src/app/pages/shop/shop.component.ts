import { Component } from '@angular/core';

import { ProductsComponent } from '../../components/products/products.component';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductsComponent],
  templateUrl: './shop.component.html',
})
export class ShopComponent {}
