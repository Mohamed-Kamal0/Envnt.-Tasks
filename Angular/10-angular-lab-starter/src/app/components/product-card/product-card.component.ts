import { Component, Input } from '@angular/core';

import { Product } from '../../models/product';

@Component({
  selector: 'app-product-card',
  standalone: true,
  templateUrl: './product-card.component.html',
})
export class ProductCardComponent {
  @Input({ required: true }) productData!: Product;
}
