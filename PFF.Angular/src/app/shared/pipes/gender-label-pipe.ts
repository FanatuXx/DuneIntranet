import { Pipe, PipeTransform } from '@angular/core';
import { Gender } from '../enums/gender.enum';

@Pipe({
  name: 'GenderLabelPipe',
  standalone: true
})

export class GenderLabelPipe implements PipeTransform {
  transform(gender: keyof typeof Gender | null | undefined): string {
      if (!gender) {
        return '';
      }

      return Gender[gender];
    }
}
