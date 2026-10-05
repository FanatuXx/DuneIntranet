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
            .then(f => f.PatientsList),
        title: "Dune Intranet | Liste des usagers"
    },
    {
        path: ':id',
        loadComponent: () => import("./patient-details/patient-details")
            .then(f => f.PatientDetails),
        title: "Dune Intranet | Détails usagers"
    },
    {
        path: ':id/edit',
        loadComponent: () => import("./patient-update/patient-update")
            .then(f => f.PatientUpdate),
        title: "Dune Intranet | Détails usagers"
    },
    {
        path: ':id/delete',
        loadComponent: () => import("./patient-delete/patient-delete")
            .then(f => f.PatientDelete)
    },


]