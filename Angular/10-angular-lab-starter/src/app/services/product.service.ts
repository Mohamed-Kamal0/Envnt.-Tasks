import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../environments/environment';
import { Product } from '../models/product';

/**
 * Some .NET controllers wrap the payload in an envelope, e.g.
 *   { "statusCode": 200, "message": "...", "data": [ ... ] }
 * instead of returning the bare array/object. The unwrap helpers below
 * handle both shapes so the component never has to know which one the API
 * actually uses.
 */
@Injectable({ providedIn: 'root' })
export class ProductService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/products`;

  getProducts(): Observable<Product[]> {
    return this.http
      .get<Product[] | Record<string, unknown>>(this.baseUrl)
      .pipe(map((response) => this.unwrapList(response)));
  }

  getProductById(id: number): Observable<Product> {
    return this.http
      .get<Product | Record<string, unknown>>(`${this.baseUrl}/${id}`)
      .pipe(map((response) => this.unwrapOne(response)));
  }

  private unwrapList(response: Product[] | Record<string, unknown>): Product[] {
    if (Array.isArray(response)) {
      return response;
    }

    for (const key of ['data', 'items', 'value', 'result', 'results']) {
      const candidate = response[key];
      if (Array.isArray(candidate)) {
        return candidate as Product[];
      }
    }

    console.warn(
      'ProductService: expected an array (or a { data: [...] } envelope) but got:',
      response
    );
    return [];
  }

  private unwrapOne(response: Product | Record<string, unknown>): Product {
    if (response && typeof response === 'object' && 'id' in response) {
      return response as Product;
    }

    for (const key of ['data', 'value', 'result']) {
      const candidate = (response as Record<string, unknown>)[key];
      if (candidate && typeof candidate === 'object') {
        return candidate as Product;
      }
    }

    console.warn('ProductService: unexpected product shape from API:', response);
    return response as unknown as Product;
  }
}
