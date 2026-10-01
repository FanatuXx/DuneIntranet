import { Pipe, PipeTransform } from '@angular/core';
import { DrugType } from '../enums/drug-type.enum';

@Pipe({
  name: 'DrugLabelPipe',
  standalone: true
})

export class DrugLabelPipe implements PipeTransform {
  transform(drug: keyof typeof DrugType | null | undefined): string {
    if (!drug) {
      return '';
    }

    return DrugType[drug];
  }
}
