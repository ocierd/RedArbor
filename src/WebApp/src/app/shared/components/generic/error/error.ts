import { Component, computed, inject, Injector, input, OnInit, Signal, signal } from '@angular/core';
import { FieldTree, FormField, required } from '@angular/forms/signals';
import { MatFormField, MatFormFieldControl } from '@angular/material/form-field';
import { min } from 'rxjs';

@Component({
  selector: '[app-error]',
  standalone: false,
  templateUrl: './error.html',
  styleUrl: './error.scss',
})
export class Error<T extends string | number> implements OnInit {

  readonly errorMessages = {
    required: 'This field is required',
    minLength: 'Minimum length not met',
    maxLength: 'Maximum length exceeded',


  };


  field = input<FieldTree<T>>();
  injector: Injector = inject(Injector);

  error: Signal<string> = computed(() => {
    const state = this.field()
    if (!state) {
      return ''
    }
    const errors = state().errors();
    if (!errors) {
      return '';
    }
    for (let iE = 0; iE < errors.length; iE++) {
      const err = errors[iE];
      switch (err.kind) {
        case 'required':
          return 'This field is required';
        case 'minLength':
          return `Minimum length not met`;
        case 'maxLength':
      }
      

    }

    return `Errors: ${JSON.stringify(errors)}`;
  });

  ngOnInit(): void {

    const matFormField = this.injector.get(MatFormField, null);
    if (matFormField) {
      console.log('Mat form field control: ', matFormField);

    }


    const state = this.field();
    if (!state) {
      console.log("No form field provided");
      return;
    }
    const value = state().controlValue();
    console.log("Form field error component VALUE: ", value);

  }

}
