import { Component, inject, input, OnInit, signal } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PatientsService } from '../../../core/services/patients.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Gender } from '../../../shared/enums/gender.enum';
import { ResidenceStatus } from '../../../shared/enums/residence-status.enum';
import { DrugType } from '../../../shared/enums/drug-type.enum';
import { ConsumptionFrequency } from '../../../shared/enums/consumption-frequency.enum';
import { legalAgeValidator } from '../../../shared/validators/legal-age.validator';
import { Patient } from '../../../shared/models/patient.model';
import { DatePipe } from '@angular/common';

import { DatePickerModule } from '@openng/optimus-ui/datepicker';

import { finalize } from 'rxjs';

@Component({
  imports: [ReactiveFormsModule, DatePickerModule],
  selector: 'app-patient-update',
  styleUrl: './patient-update.css',
  templateUrl: './patient-update.html',
})

export class PatientUpdate implements OnInit {

  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly patientsService: PatientsService = inject(PatientsService);
  private readonly formBuilder: FormBuilder = inject(FormBuilder);
  private readonly router = inject(Router);

  readonly patient = signal<Patient | null>(null);
  readonly loading = signal<boolean>(true);
  readonly error = signal<string | null>(null);

  constructor(public datepipe: DatePipe){}

  readonly id = input.required<number>();
  readonly maxBirthDate = new Date();

  deletedConsumptionIds: number[] = [];

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
      firstName: ['', [Validators.minLength(1), Validators.maxLength(50)]],
      lastName: ['', [Validators.minLength(1), Validators.maxLength(50)]],
      alias: ['', [Validators.minLength(1), Validators.maxLength(50)]],
      gender: [''],
      birthDate: ['', legalAgeValidator()],
      phoneNumber: ['', [Validators.minLength(9), Validators.maxLength(16)]],
      allergies: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      isInsured: [''],
      insurance: ['', [Validators.minLength(2), Validators.maxLength(50)]],
      hasInsuranceCard: [''],
      insuranceCardEndDate: [''],
      isAtFedasil: [''],
      income: ['', [Validators.min(0)]],
      status: [''],
      isWorking: [''],
      consumptions: this.formBuilder.array([   //Permet de créer un form imbriqué dans mon form principal
        this.updateConsumption()
      ]),
      patientAddress: this.formBuilder.group({ //Permet de créer un form imbriqué dans mon form principal
        street: ['', [Validators.minLength(1), Validators.maxLength(256)]],
        number: ['', [Validators.minLength(1), Validators.maxLength(10)]],
        zipCode: ['', [Validators.minLength(1), Validators.maxLength(5)]],
        town: ['', [Validators.minLength(1), Validators.maxLength(128)]],
        country: ['', [Validators.minLength(1), Validators.maxLength(128)]]
      })
    });

  this.loading.set(true);

  this.patientsService.getById(this.id())
    .pipe(
      finalize(() => this.loading.set(false)) //Set loading to false either way the request succees or fails
    )
    .subscribe({
      next: (patient: Patient) => {
        this.patient.set(patient);

        this.form.patchValue({
          ...patient,
          birthDate: patient.birthDate
            ? new Date(patient.birthDate)
            : null,

          insuranceCardEndDate: patient.insuranceCardEndDate
            ? new Date(patient.insuranceCardEndDate)
            : null,
          patientAddress: patient.patientAddress ?? {
            street: '',
            number: '',
            zipCode: '',
            town: '',
            country: ''
          }
        });

        // Reconstruire le FormArray des consommations
        this.consumptions.clear({ emitEvent: false });

        if (patient.consumptions?.length) {
          patient.consumptions.forEach(consumption => {
            const consumptionForm = this.updateConsumption();

            consumptionForm.patchValue({
              id: consumption.id,
              drugType: consumption.drugType ?? '',
              frequency: consumption.frequency ?? ''
            });

            this.consumptions.push(consumptionForm, {
              emitEvent: false
            });
          });
        } else {
          this.consumptions.push(this.updateConsumption(), {
            emitEvent: false
          });
        }
      },

      error: (err) => {
        console.error('Erreur de chargement du patient :', err);
        this.error.set(
          err?.error?.message ?? err?.message ?? 'Erreur de chargement'
        );
      }
    });
  };

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
  
  get street() {
    return this.form.get('patientAddress.street');
  }

  get number() {
    return this.form.get('patientAddress.number');
  }

  get zipCode() {
    return this.form.get('patientAddress.zipCode');
  }

  get town() {
    return this.form.get('patientAddress.town');
  }

  get country() {
    return this.form.get('patientAddress.country');
  }

  get consumptions(): FormArray {
    return this.form.get('consumptions') as FormArray;
  }

  private updateConsumption(): FormGroup {
    return this.formBuilder.group({
      id: [null],
      drugType: [''],
      frequency: ['']
    });
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

  const patientData = this.form.getRawValue();

  patientData.consumptions = patientData.consumptions.filter(
    (consumption: { drugType: string; frequency: string }) =>
      consumption.drugType && consumption.frequency
  );

  const payload = {
    ...patientData,
    consumptionsToDeleteIds: this.deletedConsumptionIds
  };

  this.patientsService.update(this.id(), payload).subscribe({
    next: () => this.router.navigate(['patients']),
    error: (err) => console.error('Erreur de mise à jour :', err),
  });
  }

  addConsumption(): void 
  {
    this.consumptions.push(this.updateConsumption());
  }

  removeConsumption(index: number): void {
    const consumption = this.consumptions.at(index);
    const id = consumption.get('id')?.value;

    if (id != null) {
      this.deletedConsumptionIds.push(id);
    }

    if (this.consumptions.length > 1) {
      this.consumptions.removeAt(index);
    } else {
      this.consumptions.at(0).reset({
        id: null,
        drugType: '',
        frequency: ''
      });
    }
  }
}