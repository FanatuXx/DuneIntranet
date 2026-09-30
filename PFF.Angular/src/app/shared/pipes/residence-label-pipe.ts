import { Pipe, PipeTransform } from '@angular/core';
import { ResidenceStatus } from '../enums/residence-status.enum';

@Pipe({
  name: 'ResidenceLabelPipe',
  standalone: true
})

export class ResidenceLabelPipe implements PipeTransform {
  transform(
    status: keyof typeof ResidenceStatus | null | undefined
  ): string {
    if (!status) {
      return '';
    }

    return ResidenceStatus[status];
  }
}
