import { inject, Injector, Pipe, PipeTransform } from '@angular/core';
import { PipeType } from '@models/gui/grid-model';

/**
 * Pipe that allows to apply a pipe dynamically based on the column configuration in a grid
 * It receives the value and the pipe configuration (type and args) and applies the pipe to the value
 * Usage example: <td>{{ row.createdAt | dynamic: column.pipe }}</td>
 */
@Pipe({
  name: 'dynamic',
  standalone: false
})
export class DynamicPipe implements PipeTransform {
  injector = inject(Injector);

  transform(value: unknown, pipe?: PipeType): any {
    if (value && pipe) {
      const pipeInstance = this.injector.get(pipe.type);
      const args = Array.isArray(pipe.args) ? pipe.args : (pipe.args != undefined ? [pipe.args] : []);
      return pipeInstance.transform(value, ...args);
    }
    return value;
  }

}