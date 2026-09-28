import { Component, inject, input, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PatientsService } from '../../../core/services/patients.service';
import { Patient } from '../../../shared/models/patient.model';

@Component({
  imports: [RouterLink],
  selector: 'app-patient-details',
  styleUrl: './patient-details.css',
  templateUrl: './patient-details.html',
})
export class PatientDetails implements OnInit {
  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly patientsService = inject(PatientsService);

  readonly id = input.required<number>();

  readonly patient = signal<Patient | null>(null);
  readonly loading = signal<boolean>(true);
  readonly error = signal<string | null>(null);

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
}
