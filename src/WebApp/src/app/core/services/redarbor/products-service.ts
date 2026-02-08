import { Injectable } from '@angular/core';
import { Product, ProductsFilter } from '@models/products.model';
import { RedarborBaseService } from '@services/bases/redarbor-base-service';
import { Observable } from 'rxjs';

/**
 * Service to manage products related operations
 */
@Injectable({
  providedIn: 'root',
})
export class ProductsService extends RedarborBaseService {

  constructor() {
    super('Products');
  }

  /**
   * Get all products
   * @returns Products list
   */
  getAllProducts(): Observable<Product[]> {
    return this.get('');
  }

  /**
   * Get products filtered by criteria
   * @param filter Filter criteria
   * @returns Filtered products
   */
  getFilteredProducts(filter: ProductsFilter): Observable<Product[]> {
    return this.post('/filtered', filter);
  }

}

