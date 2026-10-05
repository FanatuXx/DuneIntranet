import { Component, inject, input, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PatientsService } from '../../../core/services/patients.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Gender } from '../../../shared/enums/gender.enum';
import { ResidenceStatus } from '../../../shared/enums/residence-status.enum';
import { DrugType } from '../../../shared/enums/drug-type.enum';
import { ConsumptionFrequency } from '../../../shared/enums/consumption-frequency.enum';
import { legalAgeValidator } from '../../../shared/validators/legal-age.validator';
import { Patient } from '../../../shared/models/patient.model';
import { DatePipe } from '@angular/common';

@Component({
  imports: [ReactiveFormsModule],
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
      firstName: [''],
      lastName: [''],
      alias: [''],
      gender: [''],
      birthDate: ['', legalAgeValidator()],
      phoneNumber: ['', [Validators.minLength(9), Validators.maxLength(16)]],
      allergies: [''],
      isInsured: [''],
      insurance: ['', [Validators.minLength(2), Validators.maxLength(50)]],
      hasInsuranceCard: [''],
      insuranceCardEndDate: [''],
      isAtFedasil: [''],
      income: ['', [Validators.min(0)]],
      status: [''],
      isWorking: [''],
      drugType: [''],
      consumptionFrequency: ['']
    });

    this.patientsService.getById(this.id()).subscribe({
      next: (patient: Patient) => {
        this.patient.set(patient); //Permet d'utiliser des propriétés de patient dans le titre 
        this.form.patchValue({
          ...patient,
          birthDate: this.datepipe.transform(patient.birthDate, 'yyyy-MM-dd'),
          insuranceCardEndDate: this.datepipe.transform(patient.insuranceCardEndDate, 'yyyy-MM-dd'),
        });
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err.message ?? 'Erreur de chargement');
        this.loading.set(false);
      },
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
  
  get drugType() {
    return this.form.controls['drugType'];
  }

  get consumptionFrequency() {
    return this.form.controls['consumptionFrequency'];
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

    this.patientsService.update(this.id(), this.form.getRawValue()).subscribe({
      next: () => this.router.navigate(['patients']),
      error: (err) => console.log('Erreur: ', err),
    });
  }
}