import { Component, inject, input, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PatientsService } from '../../../core/services/patients.service';
import { Patient } from '../../../shared/models/patient.model';
import { DatePipe } from '@angular/common';
import { ResidenceStatus } from '../../../shared/enums/residence-status.enum';
import { ResidenceLabelPipe } from '../../../shared/pipes/residence-label-pipe';
import { GenderLabelPipe } from '../../../shared/pipes/gender-label-pipe';
import { ConsumptionLabelPipe } from '../../../shared/pipes/consumption-label-pipe';
import { DrugLabelPipe } from '../../../shared/pipes/drug-label-pipe';
import { Gender } from '../../../shared/enums/gender.enum';
import { DrugType } from '../../../shared/enums/drug-type.enum';
import { ConsumptionFrequency } from '../../../shared/enums/consumption-frequency.enum';

@Component({
  imports: [ResidenceLabelPipe, GenderLabelPipe, ConsumptionLabelPipe, DrugLabelPipe ],
  selector: 'app-patient-details',
  styleUrl: './patient-details.css',
  templateUrl: './patient-details.html',
})
export class PatientDetails implements OnInit {
  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly patientsService = inject(PatientsService);

  constructor(public datepipe: DatePipe){}

  readonly id = input.required<number>();

  readonly patient = signal<Patient | null>(null);
  readonly loading = signal<boolean>(true);
  readonly error = signal<string | null>(null);

  ResidenceStatus = ResidenceStatus;
  Gender = Gender;
  DrugType = DrugType;
  ConsumptionFrequency = ConsumptionFrequency;

  ngOnInit(): void {
    console.log('this.activatedRoute :>> ', this.activatedRoute);
    console.log(this.activatedRoute.snapshot.params['id']);
    console.log('id :>> ', this.id());

    this.patientsService.getById(this.id()).subscribe({
      next: (patient: Patient) => {
        this.patient.set(patient);
      },
      error: (err) => {
        this.error.set(err);
      },
      complete: () => {
        this.loading.set(false);
      },
    });
  }

  convertDate(date: Date) {
    return this.datepipe.transform(date, 'dd/MM/yyyy');
  }
}
