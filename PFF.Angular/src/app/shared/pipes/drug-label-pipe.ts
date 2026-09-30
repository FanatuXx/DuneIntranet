import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'DrugLabelPipe',
})
export class DrugLabelPipe implements PipeTransform {
  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }
}
