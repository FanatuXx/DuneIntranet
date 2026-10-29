import { Component, OnInit, inject } from '@angular/core';
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

  // Action exécutée lorsque l'utilisateur clique sur une suggestion
  selectEntry(entry: PatientEntry): void {
    // 1. Met à jour le champ texte avec la valeur lisible
    this.searchPatientForm.controls.searchQuery.setValue(entry.alias, { emitEvent: false });
    
    // 2. Stocke l'identifiant unique (clé primaire) dans le champ masqué ou technique
    this.searchPatientForm.controls.selectedId.setValue(entry.id.toString());
  }

  onSubmit(): void {
    if (this.searchPatientForm.valid) {
      console.log('Données du formulaire prêtes pour l\'envoi :', this.searchPatientForm.value);
      // Exemple : { searchQuery: 'Alice', selectedId: '42' }
    }
  }
}