import { Component, OnInit, inject, Output, EventEmitter } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Observable, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, catchError, tap } from 'rxjs/operators';
import { PatientsService } from '../../../core/services/patients.service';
import { PatientEntry } from '../../../shared/models/patient.model';
import { DatePipe } from '@angular/common';
import { InputTextModule } from '@openng/optimus-ui/inputtext';

@Component({
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, DatePipe],
  selector: 'app-patient-search-bar',
  styleUrl: './patient-search-bar.css',
  templateUrl: './patient-search-bar.html',
})

export class PatientSearchBar implements OnInit {

  @Output() patientSelected = new EventEmitter<number>();

  private fb = inject(NonNullableFormBuilder);
  private readonly patientsService: PatientsService = inject(PatientsService);

  // Déclaration du Reactive Form
  searchPatientForm = this.fb.group({
    searchQuery: [''], // Le champ de texte pour la saisie de recherche
    selectedId: ['', Validators.required] // L'ID final de l'entrée sélectionnée
  });

  // Flux observable qui contiendra les suggestions de la base de données
  filteredResults$!: Observable<PatientEntry[]>;
  isLoading = false;

  ngOnInit(): void {
    this.filteredResults$ = this.searchPatientForm.controls.searchQuery.valueChanges.pipe(
      debounceTime(300),         // Attend 300ms de pause dans la saisie avant de lancer la requête
      distinctUntilChanged(),   // N'émet que si la valeur a réellement changé
      tap(() => this.isLoading = true),
      switchMap(query => {
        if (!query.trim() || query.length < 2) {
          this.isLoading = false;
          return of([]);        // Retourne un tableau vide si la saisie est trop courte
        }
        // Appel à votre API Backend (Filtre SQL exécuté côté serveur)
        return this.patientsService.getSearch(query).pipe(
          tap(() => this.isLoading = false),
          catchError(() => {
            this.isLoading = false;
            return of([]);      // Gestion des erreurs : retourne un tableau vide
          })
        );
      })
    );
  }

  selectEntry(entry: PatientEntry): void {
    const birthDate = entry.birthDate
      ? new Date(entry.birthDate).toLocaleDateString('fr-BE')
      : '';

    const searchValue = `${entry.alias}${
      birthDate ? ' — Né(e) le ' + birthDate : ''
    }`;

    this.searchPatientForm.patchValue({
      searchQuery: searchValue,
      selectedId: String(entry.id)
    });

    this.patientSelected.emit(entry.id);
  }
}