import { Component, input, InputSignal, output, OutputEmitterRef } from '@angular/core';


/**
 * SpinnerButton component
 * A button that shows a spinner when loading
 */
@Component({
  selector: 'app-spinner-button',
  standalone: false,
  templateUrl: './spinner-button.html',
  styleUrl: './spinner-button.scss',
})
export class SpinnerButton {

  /**
   * Event emitted when the button is clicked
   */
  clic: OutputEmitterRef<MouseEvent> = output<MouseEvent>();

  /**
   * Whether the button is in loading state or not. When true, the button shows a spinner and is disabled
   */
  isLoading: InputSignal<boolean> = input(false);

  /**
   * Text to show in the button when not loading. 
   * When loading, the spinner is shown instead of the text
   */
  text: InputSignal<string> = input('');

  /**
   * Handles the click event on the button
   * Only emmits when not loading, otherwise does nothing
   * @param evt Mouse event
   */
  onClick(evt: MouseEvent) {
    if (!this.isLoading()) {
      this.clic.emit(evt);
    }
  }

}
