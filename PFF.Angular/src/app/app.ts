import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBar } from './layout/nav-bar/nav-bar';
import { PatientCreation } from './features/patients/patient-creation/patient-creation';
import { Dashboard } from './features/dashboard/dashboard';
import { DatePipe } from '@angular/common';
import { GenderLabelPipe } from './shared/pipes/gender-label-pipe';
import { DrugLabelPipe } from './shared/pipes/drug-label-pipe';
import { ConsumptionLabelPipe } from './shared/pipes/consumption-label-pipe';
import { ResidenceLabelPipe } from './shared/pipes/residence-label-pipe';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavBar],
  providers: [DatePipe, ConsumptionLabelPipe, DrugLabelPipe, GenderLabelPipe, ResidenceLabelPipe],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('PFF.Angular');
}
