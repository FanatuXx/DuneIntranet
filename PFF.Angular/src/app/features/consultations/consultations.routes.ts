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
    }
]