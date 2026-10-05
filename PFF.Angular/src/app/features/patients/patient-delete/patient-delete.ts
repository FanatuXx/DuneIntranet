import { Component, inject, input, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { PatientsService } from '../../../core/services/patients.service';
import { Patient } from '../../../shared/models/patient.model';

@Component({
  imports: [],
  selector: 'app-patient-delete',
  styleUrl: './patient-delete.css',
  templateUrl: './patient-delete.html',
})
export class PatientDelete implements OnInit{

  private readonly patientsService: PatientsService = inject(PatientsService);
  private readonly router = inject(Router);

  readonly patient = signal<Patient | null>(null);

  constructor(){}

  readonly id = input.required<number>();

  ngOnInit(): void {
    this.patientsService.delete(this.id()).subscribe({
    next: () => this.router.navigate(['patients']),
    error: (err) => console.log('Erreur: ', err),
    });
  }
}
