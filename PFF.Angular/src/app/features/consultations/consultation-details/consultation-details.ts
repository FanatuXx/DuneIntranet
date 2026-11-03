import { Component, inject, input, OnInit, signal } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';
import { finalize } from 'rxjs';
import { Consultation } from '../../../shared/models/consultation.model';
import { ConsultationsService } from '../../../core/services/consultations.service';

@Component({
  imports: [RouterLink, RouterModule],
  selector: 'app-consultation-details',
  styleUrl: './consultation-details.css',
  templateUrl: './consultation-details.html',
})


export class ConsultationDetails implements OnInit {
  private readonly consultationsService = inject(ConsultationsService);

  constructor(public datepipe: DatePipe){}

  readonly id = input.required<number>();

  readonly consultation = signal<Consultation | null>(null);
  readonly loading = signal<boolean>(true);
  readonly error = signal<string | null>(null);

  ngOnInit(): void {

  this.consultationsService.getById(this.id())
    .pipe(finalize(() => this.loading.set(false)))
    .subscribe({
      next: (consultation: Consultation) => {
        this.consultation.set(consultation);
      },
      error: (err) => {
        this.error.set(
          err?.error?.message ??
          err?.message ??
          'Une erreur est survenue lors du chargement de la consultation.'
        );
      }
    });
  }

  convertDate(date: Date | string | null | undefined): string | null {
    if (!date) {
      return null;
    }

    return this.datepipe.transform(date, 'dd/MM/yyyy');
  }
}
