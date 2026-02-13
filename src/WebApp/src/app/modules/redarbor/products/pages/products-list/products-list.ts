import { AfterViewInit, Component, effect, inject, OnDestroy, signal, Signal, WritableSignal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { debounce, disabled, FieldState, FieldTree, form } from '@angular/forms/signals';
import { Product, ProductsFilterData } from '@models/products.model';
import { ProductsService } from '@services/redarbor/products-service';
import { filter, firstValueFrom, Subject, } from 'rxjs';
import { AlertsService } from '../../../../../shared/services/alerts-service';
import { LoggerService } from '@shared-services/logger-service';

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
export class ProductsList implements AfterViewInit, OnDestroy {

  private logger = inject(LoggerService);

  columns = [
    { field: 'productId', header: 'ID' },
    { field: 'name', header: 'Name' },
    { field: 'description', header: 'Description' },
    { field: 'price', header: 'Price' },
    { field: 'createdAt', header: 'Created At' },
    { field: 'otra.nested.property', header: 'Nested Property' }
  ];

  filterData: WritableSignal<ProductsFilterData> = signal({ ...RESET_DATA });
  currentFilter: WritableSignal<ProductsFilterData | null> = signal(null);
  loadingFilter: WritableSignal<boolean> = signal(false);

  filterForm: FieldTree<ProductsFilterData> = form<ProductsFilterData>(this.filterData, opts => {
    debounce(opts, 500);
    disabled(opts, this.loadingFilter);
  });

  productsService = inject(ProductsService);
  productsFilterSubject: Subject<Product[]> = new Subject();

  productz: Signal<Product[]> = toSignal(this.productsFilterSubject
    , { initialValue: [] });


  alertsService = inject(AlertsService);

  lastFocusedField: FieldTree<string | number> | null = null;
  listenersAdded: { [key: string]: (evt: Event) => any } = {};

  constructor() {

    effect(() => {
      const filterValue = this.filterData();
      const current = this.currentFilter();

      if (JSON.stringify(filterValue) === JSON.stringify(current)) {
        return;
      }
      this.currentFilter.set(filterValue);
      this.logger.info('Filter changed: ', filterValue);
      this.getProducts();
    });

  }

  /**
   * After the view is initialized, we add focus listeners to each form field to keep track of the last focused field. This allows us to restore focus after loading products.
   */
  ngAfterViewInit(): void {
    Object.keys(this.filterForm).forEach(key => {
      const field = this.filterForm[key as keyof typeof this.filterForm] as FieldTree<string | number>;
      this.logger.info(`Field ${key}, name:${key}: `, field);
      const listener = (evt: Event) => {
        this.logger.info(`Event ${evt.type} on field ${key}: `, evt);
        this.lastFocusedField = field;
      }
      field().formFieldBindings().forEach(binding => {
        binding.element.addEventListener('focus', listener);
        this.listenersAdded[key] = listener;
      });
    });
  }

  /**
   * Removes all event listeners added to the form fields to prevent memory leaks. 
   * This is important, because if the component is destroyed while an async operation is still pending, the subscription to the products filter subject could cause memory leaks.
   */
  private removeListeners() {
    Object.keys(this.listenersAdded).forEach(key => {
      const field = this.filterForm[key as keyof typeof this.filterForm] as FieldTree<string | number>;
      const listener = this.listenersAdded[key];
      field().formFieldBindings().forEach(binding => {
        binding.element.removeEventListener('focus', listener);
        this.logger.info(`Listener removed from Field ${key} bindings: `, binding);
      });
    });
  }

  /**
   * Fetches products based on the current filter data. 
   * It shows a loading state while fetching, and updates the products filter subject with the new products once they are fetched. If an error occurs, it shows an alert message. After fetching, it restores focus to the last focused field.
   */
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
      const products = await firstValueFrom(this.productsService.getFilteredProducts(dataToSend));
      products.forEach(p => {
        if (typeof p.createdAt === 'string') {
          const d = new Date(p.createdAt);
          (p as any).otra = {
            nested: { property: `${d.getDay()}/${d.getMonth() + 1}/${d.getFullYear()}` }
          }
        }
      });
      this.productsFilterSubject.next(products);
      const fs = this.filterForm();
    } catch (error) {
      this.alertsService.sendAlertMessageAsync('An error occurred while fetching products.');
    }
    finally {
      this.loadingFilter.set(false);
      const fs = this.filterForm();

      setTimeout(() => {
        if (this.lastFocusedField) {
          this.lastFocusedField().focusBoundControl();
        }
      }, 0);
    }

  }

  /**
   * Reset the filters to their default values.
   * This will trigger the effect that Listens to filterData changes and reload the products with the default filters.
   */
  resetFilters() {
    this.filterData.set({ ...RESET_DATA });
  }

  /**
   * Complete the products filter subject to avoid memory Leaks.
   * This is important, because if the component is destroyed while an async operation is still pending, the subscription to the products filter subject could cause memory Leaks.
   * Removes all event listeners added to the form fields to prevent memory leaks.
   */
  ngOnDestroy(): void {
    this.productsFilterSubject.complete();
    this.removeListeners();
  }

  /**
   * Open a confirmation alert when the user clicks the alert button. 
   * The button is disabled while loading products to prevent multiple alerts from being opened. 
   * The response from the alert is logged to the console.
   */
  async openAlert(): Promise<void> {
    this.loadingFilter.set(true);
    const response = await this.alertsService
      .sendConfirmAlertAsync('Please confirm this action.');
    this.loadingFilter.set(false);
    this.logger.info('Alert response: ', response);
  }

}
