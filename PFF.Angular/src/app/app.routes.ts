import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    },
    {
        path: 'dashboard',
        loadComponent: () => import('./features/dashboard/dashboard').then((f) => f.Dashboard),
        title: 'DUNE Intranet | Dashboard',
    },
    {
        path: 'patients',
        loadChildren: () => import('./features/patients/patients.routes').then((x) => x.routes),
        title: "Dune Intranet | Usagers",
    }

];
