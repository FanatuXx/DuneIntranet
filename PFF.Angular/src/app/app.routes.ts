import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    },
    {
        path: 'dashboard',
        loadComponent: () => import('./features/patients/patients-list/patients-list').then((f) => f.PatientsList),
        title: 'DUNE Intranet | Dashboard',
    },
    {
        path: 'patients',
        loadChildren: () => import('./features/patients/patients.routes').then((x) => x.routes),
        title: "Dune Intranet | Usagers",
    },
    {
        path: 'consultations',
        loadChildren: () => import('./features/consultations/consultations.routes').then((x) => x.routes),
        title: "Dune Intranet | Consultations",
    }


];
