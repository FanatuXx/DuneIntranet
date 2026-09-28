import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function legalAgeValidator(): ValidatorFn 
{
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) 
      return null;

    const today = new Date();
    const birthDate = new Date(control.value);
    let age = today.getFullYear() - birthDate.getFullYear();
    const m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }

    return age < 18 ? { underEighteen: true } : null;
  };
}