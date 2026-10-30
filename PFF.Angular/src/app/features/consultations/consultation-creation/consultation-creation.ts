import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ConsultationsService } from '../../../core/services/consultations.service';
import { ReactiveFormsModule, FormArray, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { WorkersService } from '../../../core/services/workers.service';
import { Worker } from '../../../shared/models/worker.model';
import { PathologiesService } from '../../../core/services/pathologies.service';
import { Pathology } from '../../../shared/models/pathology.model';
import { PatientSearchBar } from '../../patients/patient-search-bar/patient-search-bar';
import { Medicine } from '../../../shared/models/medicine.model';
import { MedicinesService } from '../../../core/services/medicines.service';
import { InputTextModule } from '@openng/optimus-ui/inputtext';
import { TextareaModule } from '@openng/optimus-ui/textarea';
import { SelectModule } from '@openng/optimus-ui/select';
import { DatePicker } from '@openng/optimus-ui/datepicker';
import { finalize } from 'rxjs';

@Component({
  imports: [PatientSearchBar, ReactiveFormsModule, InputTextModule, TextareaModule, SelectModule, DatePicker, RouterLink],
  selector: 'app-consultation-creation',
  styleUrl: './consultation-creation.css',
  templateUrl: './consultation-creation.html',
})
export class ConsultationCreation implements OnInit {

  private readonly consultationsService: ConsultationsService = inject(ConsultationsService);
  private readonly workersService: WorkersService = inject(WorkersService);
  private readonly medicinesService: MedicinesService = inject(MedicinesService);
  private readonly pathologiesService: PathologiesService = inject(PathologiesService);
  private readonly formBuilder: FormBuilder = inject(FormBuilder);
  private readonly router = inject(Router);

  constructor(
    private cdr: ChangeDetectorRef //Force workers to be loaded without the need to write an input
  ) {}

  workers: Worker[] = []; 
  selectedWorkers: number[] | null = null; 

  pathologies: Pathology[] = [];
  selectedPathologies: number[] | null = null; 

  medicines: Medicine[] = [];
  selectedmedicine: number | null = null; 

  isSubmitting = false;


  form!: FormGroup;

  ngOnInit(): void {
    
    this.loadWorkers();
    this.loadPathologies();
    this.loadMedicine();

    this.form = this.formBuilder.group({
      motive: ['', [Validators.minLength(1), Validators.maxLength(256), Validators.required]],
      subjective: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      objective: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      assessment: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      plan: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      description: ['', [Validators.minLength(1), Validators.maxLength(256)]],
      date: [null, Validators.required],
      patientId: ['', [Validators.required]],

      workersIds: this.formBuilder.array([
        this.formBuilder.control(null)
      ]),

      pathologiesIds: this.formBuilder.array([
        this.formBuilder.control(null)
      ]),

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

  get workersIds(): FormArray {
    return this.form.get('workersIds') as FormArray;
  }

  get workerOptions() {
    return this.workers.map(worker => ({
      ...worker,
      displayName: `${worker.firstName} ${worker.lastName}`
    }));
  }

  get pathologiesIds(): FormArray {
    return this.form.get('pathologiesIds') as FormArray;
  }

  get prescriptions(): FormArray {
    return this.form.get('prescriptions') as FormArray;
  }

  get vitalSigns(): FormArray {
    return this.form.get('vitalSigns') as FormArray;
  }


  // #endregion

  // #region Workers functions 

  loadWorkers(): void {
    this.workersService.getAll().subscribe({
      next: (data) => {
        this.workers = data;
        this.cdr.markForCheck(); //Force to load the workers
      },
      error: (err) => console.error('Erreur lors du chargement', err)
    });
  }

  addWorker(): void {
    this.workersIds.push(this.formBuilder.control(null));
  }

  removeWorker(index: number): void {
    this.workersIds.removeAt(index);
    if (this.workersIds.length === 0) {
      this.workersIds.push(this.formBuilder.control(null));
    }
  }

  getWorkerControl(index: number): FormControl {
    return this.workersIds.at(index) as FormControl;
  }

  // #endregion

  // #region Pathologies functions
  loadPathologies(): void {
    this.pathologiesService.getAll().subscribe({
      next: (data) => {
        this.pathologies = data;
        this.cdr.markForCheck();
      },
      error: (err) => console.error('Erreur lors du chargement', err)
    });
  }

  addPathology(): void {
    this.pathologiesIds.push(this.formBuilder.control(null));
  }

  removePathology(index: number): void {
    this.pathologiesIds.removeAt(index);
    // Optionnel : s'assurer qu'il reste toujours au moins un bloc vide
    if (this.pathologiesIds.length === 0) {
      this.pathologiesIds.push(this.formBuilder.control(null));
    }
  }
  // #endregion

  // #region Prescriptions functions

  public createPrescription(): FormGroup {
    return this.formBuilder.group({
      startDate: [null],
      endDate: [null],
      takeFrequency: [''],

      prescriberId: ['', [Validators.min(1)]],
      
      medicinesIds: this.formBuilder.array([
        this.formBuilder.control(null)
      ])
    });
  }

  addPrescription(): void {
    this.prescriptions.push(this.createPrescription());
  }

  removePrescription(index: number): void {
    this.prescriptions.removeAt(index);

    if (this.prescriptions.length === 0) {
      this.prescriptions.push(this.createPrescription());
    }
  }
  // #endregion

  // #region VitalSigns functions

  private createVitalSign(): FormGroup {
    return this.formBuilder.group({
      heartRate: [''],
      bloodPressure: [''],
      temperature: [''],
      oxygenSaturation: [''],
      height: [''],
      weight: [''],
      bloodSugar: [''],
      date: [null],
    });
  }

  addVitalSign(): void {
    this.vitalSigns.push(this.createVitalSign());
  }

  removeVitalSign(index: number): void {
    this.vitalSigns.removeAt(index);

    if (this.vitalSigns.length === 0) {
      this.vitalSigns.push(this.createVitalSign());
    }
  }
  // #endregion

  // #region Medicines functions 

  loadMedicine(): void {
    this.medicinesService.getAll().subscribe({
      next: (data) => {
        this.medicines = data;
        this.cdr.markForCheck(); //Force to load the workers
      },
      error: (err) => console.error('Erreur lors du chargement', err)
    });
  }

  // Permet de récupérer le tableau de médicaments d'une prescription donnée
  getMedicinesIds(prescriptionIndex: number): FormArray {
    const prescriptionGroup = this.prescriptions.at(prescriptionIndex) as FormGroup;
    return prescriptionGroup.get('medicinesIds') as FormArray;
  }

  getMedicineControl(
    prescriptionIndex: number,
    medicineIndex: number
  ): FormControl {
    return this.getMedicinesIds(prescriptionIndex).at(medicineIndex) as FormControl;
  }

  // Ajoute une nouvelle liste déroulante (un contrôle vide) à une prescription spécifique
  addMedicine(prescriptionIndex: number): void {
    const medicinesArray = this.getMedicinesIds(prescriptionIndex);
    medicinesArray.push(this.formBuilder.control(null));
  }

  // Supprime une liste déroulante spécifique d'une prescription spécifique
  removeMedicine(prescriptionIndex: number, medicineIndex: number): void {
    const medicinesArray = this.getMedicinesIds(prescriptionIndex);
    medicinesArray.removeAt(medicineIndex);
    
    // S'assurer qu'il reste toujours au moins une liste déroulante
    if (medicinesArray.length === 0) {
      medicinesArray.push(this.formBuilder.control(null));
    }
  }

  // #endregion

  // #region Patient functions 

  onPatientSelected(id: number): void {
    console.log('Parent : Réception de l\'ID du patient ->', id);

    const control = this.form.get('patientId');
    
    if (control && id && !isNaN(id)) {
      control.setValue(id);
      control.markAsDirty();
      control.markAsTouched();
      control.setErrors(null); // Force la suppression de l'erreur 'required'
      control.updateValueAndValidity({ emitEvent: true });
    } else if (control) {
      control.setValue(null);
      control.setErrors({ required: true });
    }

    console.log('Parent : Statut de validité du champ patientId ->', control?.status);
    console.log('Parent : Validité globale du formulaire ->', this.form.valid);
  }

  // #endregion

  onSubmit() {
    if (this.form.invalid || this.isSubmitting) {
      return;
    }

    // 2. Bloquez immédiatement les clics suivants
    this.isSubmitting = true;

    // Convert empty field into null
    Object.keys(this.form.controls).forEach(key => {
      const control = this.form.get(key);
      if (control && (control.value === '' || (typeof control.value === 'string' && control.value.trim() === ''))) {
        control.patchValue(null, { emitEvent: false });
      }
    });

    this.consultationsService.create(this.form.getRawValue())
      .pipe(finalize(() => this.isSubmitting = false))
      .subscribe({
        next: () => this.router.navigate(['consultations']),
        error: (err) => {
          console.error('Erreur de création de consultation :', err);
        }
    });
  }
}
