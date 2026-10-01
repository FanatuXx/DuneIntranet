import { Component, computed, inject, OnInit, Signal, signal } from '@angular/core';
import { PatientsService } from '../../../core/services/patients.service';
import { RouterLink } from '@angular/router';
import { Patient } from '../../../shared/models/patient.model';
import { DatePipe } from '@angular/common';

@Component({
  imports: [RouterLink],
  selector: 'app-patients-list',
  styleUrl: './patients-list.css',
  templateUrl: './patients-list.html',
})
export class PatientsList implements OnInit {

  constructor(public datepipe: DatePipe){}
  
  private readonly patientsService: PatientsService = inject(PatientsService);

  readonly patients = signal<Patient[]>([]);
  readonly error = signal<string | null>(null);
  readonly loading = signal<boolean>(true);

  orderedPatients = computed(() => {
    return this.patients().sort((a, b) => a.alias!.localeCompare(b.alias!))
  });

  ngOnInit(): void {
    this.patientsService.getAll().subscribe({
      next: (patients: Patient[]) => {
        this.patients.set(patients);
      },
      error: (error) => {
        this.error.set(error.message);
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

