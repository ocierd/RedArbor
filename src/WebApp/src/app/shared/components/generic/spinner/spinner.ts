import { Component, input, InputSignal } from '@angular/core';
import { ThemePalette } from '@angular/material/core';

/**
 * Spinner component
 * A simple spinner that can be used to indicate loading state
 */
@Component({
  selector: 'app-spinner',
  standalone: false,
  templateUrl: './spinner.html',
  styleUrl: './spinner.scss',
})
export class Spinner {

  /**
   * Diameter of the spinner, default is 20
   */
  diameter:InputSignal<number> = input(20);

  /**
   * Color of the spinner, default is 'primary'
   * Can be any of the Angular Material color palette options (primary, accent, warn) or a custom color string (e.g. '#ff0000')
   */
  color: InputSignal<ThemePalette> = input();

}
