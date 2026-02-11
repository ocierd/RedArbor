import { Component, computed, effect, inject, OnDestroy, signal, Signal, WritableSignal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { debounce, disabled, form } from '@angular/forms/signals';
import { Product, ProductsFilterData } from '@models/products.model';
import { ProductsService } from '@services/redarbor/products-service';
import { BehaviorSubject, firstValueFrom, Subject, } from 'rxjs';
import { AlertsService } from '../../../../../shared/services/alerts-service';

const RESET_DATA = {
  name: '',
  minPrice: '',
  maxPrice: '',
  categoryId: ''
}

@Component({
  selector: 'app-products-list',
  standalone: false,
  templateUrl: './products-list.html',
  styleUrl: './products-list.scss',
})
export class ProductsList implements OnDestroy {

  filterData: WritableSignal<ProductsFilterData> = signal({
    ...RESET_DATA
  });
  currentFilter: WritableSignal<ProductsFilterData | null> = signal(null);
  loadingFilter: WritableSignal<boolean> = signal(false);

  filterForm = form<ProductsFilterData>(this.filterData, opts => {
    debounce(opts, 500);
    disabled(opts, this.loadingFilter);
  });

  productsService = inject(ProductsService);
  productsFilterSubject: Subject<Product[]> = new Subject();

  productz: Signal<Product[]> = toSignal(this.productsFilterSubject
    , { initialValue: [] });


  alertsService = inject(AlertsService);

  constructor() {

    effect(() => {
      const filterValue = this.filterData();
      const current = this.currentFilter();

      if (JSON.stringify(filterValue) === JSON.stringify(current)) {
        return;
      }
      this.currentFilter.set(filterValue);
      console.log('Filter value changed: ', filterValue);
      this.getProducts();
    });

  }

  async getProducts(): Promise<void> {
    try {
      const filterValue = this.filterData();
      const dataToSend = {
        name: filterValue.name,
        minPrice: filterValue.minPrice ? Number(filterValue.minPrice) : undefined,
        maxPrice: filterValue.maxPrice ? Number(filterValue.maxPrice) : undefined,
        categoryId: filterValue.categoryId ? Number(filterValue.categoryId) : undefined,
      };
      this.loadingFilter.set(true);
      const products = firstValueFrom(this.productsService.getFilteredProducts(dataToSend));
      this.productsFilterSubject.next(await products);
      const fs = this.filterForm();
    } catch (error) {
      this.alertsService.sendAlertMessageAsync('An error occurred while fetching products.');
    }
    finally {
      this.loadingFilter.set(false);
    }

  }

  resetFilters() {
    this.filterData.set({ ...RESET_DATA });
  }

  ngOnDestroy(): void {
    this.productsFilterSubject.complete();
  }


  openAlert() {
    this.alertsService.sendAlertMessageAsync('This is an alert message!');
  }

}
