import { Component, inject, OnInit } from '@angular/core';
import { ConsultationsService } from '../../../core/services/consultations.service';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  imports: [],
  selector: 'app-consultation-creation',
  styleUrl: './consultation-creation.css',
  templateUrl: './consultation-creation.html',
})
export class ConsultationCreation implements OnInit {
  private readonly patientsService: ConsultationsService = inject(ConsultationsService);
  private readonly formBuilder: FormBuilder = inject(FormBuilder);
  private readonly router = inject(Router);

  form!: FormGroup;

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      motive: ['', [Validators.minLength(1), Validators.maxLength(256), Validators.required]],
      subjective: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      objective: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      assessment: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      plan: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      description: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      date: ['', [Validators.required]],
      patientId: ['', [Validators.required]],

      workersIds: [''],
      pathologiesIds: [''],

      prescriptions: this.formBuilder.array([
        this.createPrescription()
      ]),
      vitalSigns: this.formBuilder.array([
        this.createVitalSign()
      ])
    });
  }

  // #region Getters
  get motive() {
    return this.form.controls['motive'];
  };

  get subjective() {
    return this.form.controls['subjective'];
  };

  get objective() {
    return this.form.controls['objective'];
  };

  get assessment() {
    return this.form.controls['assessment'];
  };

  get plan() {
    return this.form.controls['plan'];
  };

  get description() {
    return this.form.controls['description'];
  };

  get date() {
    return this.form.controls['date'];
  };

  get patientId() {
    return this.form.controls['patientId'];
  };

  get workersIds() {
    return this.form.controls['workersIds'];
  };

  get pathologiesIds() {
    return this.form.controls['plan'];
  };

  get prescriptions(): FormArray {
    return this.form.get('prescriptions') as FormArray;
  }

  get vitalSigns(): FormArray {
    return this.form.get('vitalSigns') as FormArray;
  }
}
