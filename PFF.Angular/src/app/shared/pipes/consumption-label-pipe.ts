import { Pipe, PipeTransform } from '@angular/core';
import { ConsumptionFrequency } from '../enums/consumption-frequency.enum';

@Pipe({
  name: 'ConsumptionLabelPipe',
  standalone: true
})

export class ConsumptionLabelPipe implements PipeTransform {
  transform(frequency: keyof typeof ConsumptionFrequency | null | undefined): string {
    if (!frequency) {
      return '';
    }

    return ConsumptionFrequency[frequency];
  }
}
