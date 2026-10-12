# Dune Intranet - API ASP.NET et Frontend Angular

## Introduction

Ce projet est une application web composée d'un backend en ASP.NET et d'un frontend en Angular. Il a pour but d'être, à termes, utilisé par l'ASBL Dune* comme plateforme intranet afin de gérer les données de ses usagers et de toutes les actions entreprises avec ceux-ci.

*Dune est une association bruxelloise travaillant dans le domaine de la réduction des risques, et proposant un accompagnement psycho-médico-social à toute personne majeure issue de son public cible.


## Architecture

### Backend (ASP.NET)

- **Controllers** : Gèrent les requêtes HTTP.
- **Dtos** : Data Transfer Objects pour les échanges de données.
- **Infrastructure** : Extensions et configurations.
- **Model** : Entités et modèles de données.
- **Queries** : Requêtes pour récupérer des données.
- **Repositories** : Interfaces pour accéder aux données.
- **Services** : Logique métier.

### Frontend (Angular)

- **Components** : Composants réactifs.
- **Services** : Services pour les appels API.
- **Modules** : Modules Angular pour l'organisation des composants.
- **Layout** : Composants de la barre de navigation.
- **Shared** : Composants et services partagés.

## API

### Endpoints

- **GET /api/patients** : Récupère la liste des patients.
- **POST /api/patients** : Crée un nouveau patient.
- **GET /api/patients/{id}** : Récupère les détails d'un patient.
- **PUT /api/patients/{id}** : Met à jour un patient.
- **DELETE /api/patients/{id}** : Supprime un patient.