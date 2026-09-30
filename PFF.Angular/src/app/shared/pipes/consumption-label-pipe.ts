import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ConsumptionLabelPipe',
})
export class ConsumptionLabelPipe implements PipeTransform {
  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }
}
