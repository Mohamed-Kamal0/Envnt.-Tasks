import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Product } from "../../models/product";
@Component({
  selector: "app-product-card",
  standalone: true,
  imports: [],
  templateUrl: "./product-card.component.html",
  styleUrl: "./product-card.component.css",
})
export class ProductCardComponent {
  @Input({ required: true }) productData!: Product;
  @Output() add = new EventEmitter();
  onClick() {
    this.add.emit(this.productData);
  }
}
