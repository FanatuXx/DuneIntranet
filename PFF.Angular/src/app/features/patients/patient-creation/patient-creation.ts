import { Component, inject, OnInit } from '@angular/core';
import { PatientsService } from '../../../core/services/patients.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { legalAgeValidator } from '../../../shared/validators/legal-age.validator';
import { Gender } from '../../../shared/enums/gender.enum';
import { DrugType } from '../../../shared/enums/drug-type.enum';
import { ConsumptionFrequency } from '../../../shared/enums/consumption-frequency.enum';
import { ResidenceStatus } from '../../../shared/enums/residence-status.enum';

@Component({
  selector: 'app-patient-creation',
  imports: [ ReactiveFormsModule],
  templateUrl: './patient-creation.html',
  styleUrl: './patient-creation.css',
})

export class PatientCreation implements OnInit {

  private readonly patientsService: PatientsService = inject(PatientsService);
  private readonly formBuilder: FormBuilder = inject(FormBuilder);
  private readonly router = inject(Router);

  genderOptions = Object.entries(Gender).map(([value, label]) => ({
  value,
  label
  }));

  statusOptions = Object.entries(ResidenceStatus).map(([value, label]) => ({
  value,
  label
  }));

  drugOptions = Object.entries(DrugType).map(([value, label]) => ({
  value,
  label
  }));

  consumptionFrequencyOptions = Object.entries(ConsumptionFrequency).map(([value, label]) => ({
  value,
  label
  }));

  form!: FormGroup;

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      ssin: ['', [Validators.minLength(11), Validators.maxLength(15)]],
      idNumber: ['', [Validators.minLength(12), Validators.maxLength(14)]],
      firstName: ['', Validators.minLength(1), Validators.maxLength(50)],
      lastName: ['', Validators.minLength(1), Validators.maxLength(50)],
      alias: ['', Validators.minLength(1), Validators.maxLength(50), Validators.required],
      gender: [''],
      birthDate: ['', legalAgeValidator(), Validators.required],
      phoneNumber: ['', [Validators.minLength(9), Validators.maxLength(16)]],
      allergies: ['', Validators.minLength(1), Validators.maxLength(256)],
      isInsured: [''],
      insurance: ['', [Validators.minLength(2), Validators.maxLength(50)]],
      hasInsuranceCard: [''],
      insuranceCardEndDate: [''],
      isAtFedasil: [''],
      income: ['', [Validators.min(0)]],
      status: [''],
      isWorking: [''],
      drugType: [''],
      consumptionFrequency: [''],
    });
  }

  get ssin() {
    return this.form.controls['ssin'];
  };
  
  get idNumber() {
    return this.form.controls['idNumber'];
  }
  
  get firstName() {
    return this.form.controls['firstName'];
  }
  
  get lastName() {
    return this.form.controls['lastName'];
  }
  
  get alias() {
    return this.form.controls['alias'];
  }
  
  get gender() {
    return this.form.controls['gender'];
  }
  
  get birthDate() {
    return this.form.controls['birthDate'];
  }
  
  get phoneNumber() {
    return this.form.controls['phoneNumber'];
  }
  
  get allergies() {
    return this.form.controls['allergies'];
  }
  
  get isInsured() {
    return this.form.controls['isInsured'];
  }
  
  get insurance() {
    return this.form.controls['insurance'];
  }
  
  get hasInsuranceCard() {
    return this.form.controls['hasInsuranceCard'];
  }
  
  get insuranceCardEndDate() {
    return this.form.controls['insuranceCardEndDate'];
  }
  
  
  get isAtFedasil() {
    return this.form.controls['isAtFedasil'];
  }
  
  get income() {
    return this.form.controls['income'];
  }
  
  get status() {
    return this.form.controls['status'];
  }
  
  get isWorking() {
    return this.form.controls['isWorking'];
  }
  
  get drugType() {
    return this.form.controls['drugType'];
  }


  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    //Convert empty field into null
    Object.keys(this.form.controls).forEach(key => {
      const control = this.form.get(key);
      if (control && (control.value === '' || (typeof control.value === 'string' && control.value.trim() === ''))) {
        control.patchValue(null, { emitEvent: false }); // emitEvent: false prevents unnecessary valueChange triggers
      }
    });
    
    //Automatically consider untouched checkbox as false
    Object.keys(this.form.controls).forEach(key => {
      if (key == "isWorking" || key == "isAtFedasil" || key == "hasInsuranceCard" || key == "isInsured") {
        const control = this.form.get(key);
        if (control && (control.value === null )) {
          control.patchValue(false, { emitEvent: false });
        }
      }
    });


    this.patientsService.create(this.form.getRawValue()).subscribe({
      next: () => this.router.navigate(["patients"]),
      error: (err) => console.log("Erreur: ", err)
    })
  }
}


//// TEMPLATE TO RECEIVE ALL USEFUL INFO IF SUBMITTING FAILS
//
//   onSubmit() {
//   console.log('Submit appelé');
//   console.log('Formulaire valide ?', this.form.valid);
//   console.log('Formulaire invalide ?', this.form.invalid);
//   console.log('Valeurs :', this.form.getRawValue());
//   console.log('Erreurs :', this.form.errors);
//   console.log('Erreur naissance :', this.form.controls['birthDate'].errors);

//   if (this.form.invalid) {
//     this.form.markAllAsTouched();

//     Object.keys(this.form.controls).forEach(key => {
//       const control = this.form.get(key);

//       if (control?.invalid) {
//         console.log(`Erreur sur ${key}:`, control.errors);
//       }
//     });

//     return;
//   }

//   this.patientsService.create(this.form.getRawValue()).subscribe({
//     next: () => {
//       console.log('Patient créé');
//       this.router.navigate(['/dashboard']);
//     },
//     error: (err) => {
//       console.error('Erreur API :', err);
//     }
//   });
// }