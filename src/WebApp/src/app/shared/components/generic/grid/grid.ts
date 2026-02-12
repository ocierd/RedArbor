import { Component, computed, input, InputSignal, signal, Signal } from '@angular/core';
import { GridColumn } from '@models/gui/grid-model';

const EXTENDED_PROPERTY_SEPARATOR_REGEX = /\./g; // Matches dots that are not inside square brackets

const EXTENDED_PROPERTY_REGEX = /^[\w]{0,}\./g; // Matches property names and array indices


/**
 * Generic grid component that can be used across the app to display tabular data.
 * It accepts a columns definition and data as input signals, and uses computed signals to determine which fields to display.
 * The getDataField method allows for accessing nested properties in the data using dot notation, making it flexible for various data structures.
 */
@Component({
  selector: 'app-grid',
  standalone: false,
  templateUrl: './grid.html',
  styleUrls: ['./grid.scss'],
})
export class Grid<T> {

  /**
   * Columns definition for the grid.
   * Each column should have a field (data property) and a header (display name).
   * Optional properties include sortable, filterable, width, and align.
   */
  columns: InputSignal<GridColumn[]> = input([] as GridColumn[]);

  /**
   * Computed signal that extracts the field names from the columns definition. 
   * This is used to determine which fields to display in the grid based on the columns configuration.
   * It maps over the columns and returns an array of field names, which can be used in the template to display the correct data.
   * For example, if columns are defined as [{ field: 'name', header: 'Name' }, { field: 'age', header: 'Age' }],
   * this signal will return ['name', 'age'].
   */
  protected columnsToDisplay: Signal<string[]> = computed(() => this.columns().map(col => col.field));


  /**
   * Data to be displayed in the grid.
   * It is an input signal that accepts an array of generic type T, which represents the rows of the grid.
   */
  gridData: InputSignal<T[]> = input([] as T[]);

  /**
   * Gets the value of a field from a row, supporting nested properties using dot notation.
   * For example, if the field is 'user.name', it will return row['user']['name'].
   * It also supports array indices, such as 'items[0].name'.
   * @param row Row to extract data
   * @param field Field name or composed (e.g. this.is.nested.property)
   * @returns Field value
   */
  protected getDataField(row: T, field: string): any {
    if (EXTENDED_PROPERTY_SEPARATOR_REGEX.test(field)) {
      const newField = field.replace(EXTENDED_PROPERTY_REGEX, '');
      const removed = EXTENDED_PROPERTY_REGEX.exec(field);
      if (removed && removed.length) {
        const removedField = removed[0].slice(0, -1);
        const newValue = (row as any)[removedField];
        return this.getDataField(newValue, newField);
      }
      return this.getDataField(row, newField);
    }
    var value = row[field as keyof T];
    return value;
  }

}
