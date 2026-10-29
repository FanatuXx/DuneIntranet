import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ConsultationsService } from '../../../core/services/consultations.service';
import { ReactiveFormsModule, FormArray, FormBuilder, FormGroup, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkersService } from '../../../core/services/workers.service';
import { Worker } from '../../../shared/models/worker.model';
import { PathologiesService } from '../../../core/services/pathologies.service';
import { Pathology } from '../../../shared/models/pathology.model';
import { PatientSearchBar } from '../../patients/patient-search-bar/patient-search-bar';


@Component({
  imports: [PatientSearchBar, ɵInternalFormsSharedModule, ReactiveFormsModule],
  selector: 'app-consultation-creation',
  styleUrl: './consultation-creation.css',
  templateUrl: './consultation-creation.html',
})
export class ConsultationCreation implements OnInit {
  private readonly patientsService: ConsultationsService = inject(ConsultationsService);
  private readonly workersService: WorkersService = inject(WorkersService);
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


  form!: FormGroup;

  ngOnInit(): void {
    
    this.loadWorkers();
    this.loadPathologies();

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
    return this.form.controls['pathologiesIds'];
  };

  get prescriptions(): FormArray {
    return this.form.get('prescriptions') as FormArray;
  }

  get vitalSigns(): FormArray {
    return this.form.get('vitalSigns') as FormArray;
  }
  // #endregion

  private createPrescription(): FormGroup {
    return this.formBuilder.group({
    startDate: [''],
    endDate: [''],
    takeFrequency: [''],

    prescriberId: ['', [Validators.min(1)]],
    medicinesIds: ['']
    });
  }

  private createVitalSign(): FormGroup {
    return this.formBuilder.group({
      heartRate: [''],
      bloodPressure: [''],
      temperature: [''],
      oxygenSaturation: [''],
      height: [''],
      weight: [''],
      bmi: [''],
      bloodSugar: [''],
      date: [''],
    });
  }

  loadWorkers(): void {
    this.workersService.getAll().subscribe({
      next: (data) => {
        this.workers = data;
        this.cdr.markForCheck(); //Force to load the workers
      },
      error: (err) => console.error('Erreur lors du chargement', err)
    });
  }

  loadPathologies(): void {
    this.pathologiesService.getAll().subscribe({
      next: (data) => {
        this.pathologies = data;
        this.cdr.markForCheck();
      },
      error: (err) => console.error('Erreur lors du chargement', err)
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

  addVitalSign(): void {
    this.vitalSigns.push(this.createVitalSign());
  }

  removeVitalSign(index: number): void {
    this.vitalSigns.removeAt(index);

    if (this.vitalSigns.length === 0) {
      this.vitalSigns.push(this.createVitalSign());
    }
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    //Convert empty field into null
    Object.keys(this.form.controls).forEach(key => {
      const control = this.form.get(key);
      if (control && (control.value === '' || (typeof control.value === 'string' && control.value.trim() === ''))) {
        control.patchValue(null, { emitEvent: false }); // emitEvent: false prevents unnecessary valueChange triggers
      }
    });

    this.patientsService.create(this.form.getRawValue()).subscribe({
      next: () => this.router.navigate(['consultations']),
      error: (err) => console.log('Erreur : ', err)
    });
  }


}
