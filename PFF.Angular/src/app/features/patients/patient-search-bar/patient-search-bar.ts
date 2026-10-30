import { Component, OnInit, inject, Output, EventEmitter } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Observable, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, catchError, tap } from 'rxjs/operators';
import { PatientsService } from '../../../core/services/patients.service';
import { PatientEntry } from '../../../shared/models/patient.model';

@Component({
  imports: [CommonModule, ReactiveFormsModule],
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
    this.searchPatientForm.controls.searchQuery.setValue(entry.alias, { emitEvent: false });
    // Stockez directement l'ID sous forme de chaîne
    this.searchPatientForm.controls.selectedId.setValue(entry.id.toString());

    // Émission directe convertie de manière sécurisée
    const idValue = parseInt(entry.id.toString(), 10);
    console.log('Enfant (clic) : Émission de l\'ID ->', idValue);
    this.patientSelected.emit(idValue);
  }

  onSubmit(): void {
    if (this.searchPatientForm.valid) {
      const rawId = this.searchPatientForm.controls.selectedId.value;
      const idValue = parseInt(rawId, 10);
      
      // On n'émet que si l'ID est un nombre valide supérieur à 0
      if (!isNaN(idValue) && idValue > 0) {
        console.log('Enfant (submit) : Émission de l\'ID ->', idValue);
        this.patientSelected.emit(idValue);
      }
    }
  }
}