import { Routes } from "@angular/router";

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    },
    {
        path: 'new',
        loadComponent: () => import('./consultation-creation/consultation-creation')
            .then(f => f.ConsultationCreation),
        title: "Dune Intranet | Nouvelle consultation"
    },
    // {
    //     path: 'list',
    //     loadComponent: () => import("./consultation-list/consultation-list")
    //         .then(f => f.ConsultationList),
    //     title: "Dune Intranet | Liste des consultations"
    // },
    {
        path: ':id',
        loadComponent: () => import("./consultation-details/consultation-details")
            .then(f => f.ConsultationDetails),
        title: "Dune Intranet | Détails de laconsultation"
    },
    // {
    //     path: ':id/edit',
    //     loadComponent: () => import("./patient-update/patient-update")
    //         .then(f => f.PatientUpdate),
    //     title: "Dune Intranet | Détails usagers"
    // },
    // {
    //     path: ':id/delete',
    //     loadComponent: () => import("./patient-delete/patient-delete")
    //         .then(f => f.PatientDelete)
    // },
]