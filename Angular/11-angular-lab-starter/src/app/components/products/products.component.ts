import { Component, OnInit, inject } from '@angular/core';

import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { ProductCardComponent } from '../product-card/product-card.component';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './products.component.html',
})
export class ProductsComponent implements OnInit {
  private readonly productService = inject(ProductService);

  products: Product[] = [];
  loading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.errorMessage = '';

    this.productService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Could not reach the API. Is the .NET backend running?';
        this.loading = false;
      },
    });
  }
}
