import { HttpClient } from "@angular/common/http";
import { Product } from "../models/product";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { Injectable } from "@angular/core";
@Injectable({ providedIn: "root" })
export class productService {
  constructor(private http: HttpClient) {}
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(environment.apiUrl + "/products");
  }
  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${environment.apiUrl}/products/${id}`);
  }
}
