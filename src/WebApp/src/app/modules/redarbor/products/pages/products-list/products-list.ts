import { AfterViewInit, Component, effect, inject, OnDestroy, signal, Signal, WritableSignal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { debounce, disabled, FieldState, FieldTree, form } from '@angular/forms/signals';
import { Product, ProductsFilterData } from '@models/products.model';
import { ProductsService } from '@services/redarbor/products-service';
import { filter, firstValueFrom, pipe, Subject, } from 'rxjs';
import { AlertsService } from '../../../../../shared/services/alerts-service';
import { LoggerService } from '@shared-services/logger-service';
import { DatePipe } from '@angular/common';
import { GridColumn } from '@models/gui/grid-model';

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

  /**
   * Configuration of the columns to display in the products grid, 
   * including the field to display, the header, and optionally a pipe to apply to the data in that column.
   */
  columns: GridColumn[] = [
    { field: 'productId', header: 'ID' },
    { field: 'name', header: 'Name' },
    { field: 'description', header: 'Description' },
    { field: 'price', header: 'Price' },
    { field: 'createdAt', header: 'Created At', pipe: { type: DatePipe, args: 'dd/MM/yyyy' } },
    { field: 'otra.nested.property', header: 'Nested Property' }
  ];

  /**
   * Signals to manage the state of the product filters, the current filter applied, and the Loading state while fetching products.
   */
  filterData: WritableSignal<ProductsFilterData> = signal({ ...RESET_DATA });

  /**
   * Current filter applied to the products List.
   * This is used to compare with the filterData signal in the effect that listens to filterData changes, 
   * to avoid  making unnecessary calls to the products service when the filter data has not actually changed 
   * (because of the debounce, the effect will be triggered after 500ms of inactivity, 
   * but if the user has not actually changed the filter data, we want to avoid making a call to the products service).
   * 
   */
  currentFilter: WritableSignal<ProductsFilterData | null> = signal(null);

  /**
   * Signal to manage the Loading state while fetching products.
   * This is used to disable the filter form and the alert button while products are being fetched,
   * to prevent multiple calls to the products service and multiple alerts from beign opened at the same time.
   */
  loadingFilter: WritableSignal<boolean> = signal(false);

  /**
   * Form configuration for the product filters, using Angular's reactive forms with signals.
   * The form is configured to debounce the input changes by 500ms and to be disabled while loading products.
   * The form fields are bound to the filterData signal, so any changes in the form will update the filterData signal, 
   * which will trigger the effect that listens to filterData changes and reload the products with the new filters.
   */
  filterForm: FieldTree<ProductsFilterData> = form<ProductsFilterData>(this.filterData, opts => {
    debounce(opts, 500);
    disabled(opts, this.loadingFilter);
  });

  /**
   * Subject to manage the products to display in the grid based on the current filters.
   * The products are fetched from the products service when the filterData signal changes (after the debounce), 
   * and the new products are emitted through this subject, which is converted to a signal using the toSignal function from Angular's rxjs-interop package.
   */
  productsService = inject(ProductsService);

  /**
   * Signal that holds the list of products to display in the grid, based on the current filters.
   * This signal is created from the productsFilterSubject using the toSignal function, which allows us to use the products emitted by the subject as a signal in our component.
   * The initial value of the signal is an empty array, so that the grid will be empty until the first products are fetched.
   */
  productsFilterSubject: Subject<Product[]> = new Subject();

  /**
   * Signal that holds the list of products to display in the grid, based on the current filters. This signal is created from the productsFilterSubject using the toSignal function, which allows us to use the products emitted by the subject as a signal in our component. The initial value of the signal is an empty array, so that the grid will be empty until the first products are fetched.
   * The productsFilterSubject is updated with the new products every time the filterData signal changes (after the debounce) and the products are fetched from the products service.
   */
  productz: Signal<Product[]> = toSignal(this.productsFilterSubject
    , { initialValue: [] });

  /**
   * Inject the AlertsService to show alert messages to the user, for example when an error occurs while fetching products or when the user clicks the alert button.
   */
  alertsService = inject(AlertsService);

  /**
   * Keeps track of the last focused form field in the filter form.
   */
  lastFocusedField: FieldTree<string | number> | null = null;

  /**
   * Object that contains event listeners added to the form fields bindings, to keep track of them and remove them when the component is destroyed to prevent memory lacks.
   */
  listenersAdded: { [key: string]: (evt: FocusEvent) => any } = {};

  
  sortered = signal([0, 1, 0, 3, 0, 5].sort((next, prev) => {
    var result = prev === 0 ? Number.MIN_SAFE_INTEGER : 0;
    return result;
  }));

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

      const listener = (evt: FocusEvent) => {
        this.logger.info(`Event ${evt.type} on field ${key}: `, evt);
        this.lastFocusedField = field;
      }
      field().formFieldBindings().forEach(binding => {
        this.logger.info(`Added focus event listener to Field ${key}`);
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
