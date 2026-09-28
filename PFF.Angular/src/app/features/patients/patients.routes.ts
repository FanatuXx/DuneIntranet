import { Routes } from "@angular/router";

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    },
    {
        path: 'new',
        loadComponent: () => import('./patient-creation/patient-creation')
            .then(f => f.PatientCreation),
        title: "Dune Intranet | Ajout d'un usager"
    },
    {
        path: 'list',
        loadComponent: () => import("./patients-list/patients-list")
            .then(f => f.PatientsList)
    },
    {
        path: ':id',
        loadComponent: () => import("./patient-details/patient-details")
            .then(f => f.PatientDetails)
    },


]