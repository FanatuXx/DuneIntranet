# Dune Intranet - ASP.NET API and Angular Frontend

## Introduction

This project is a web application consisting of an ASP.NET backend and an Angular frontend. Ultimately, it is intended to be used by the non-profit organization Dune* as an intranet platform to manage data about its service users and all activities carried out with them.

*Dune is a Brussels-based organization working in the field of harm reduction and providing psycho-medico-social support to any adult who falls within its target audience.


## Architecture

### Backend (ASP.NET)

- **Controllers**: Handle HTTP requests.
- **Dtos**: Data Transfer Objects used to exchange data.
- **Infrastructure**: Extensions and configuration.
- **Model**: Entities and data models.
- **Queries**: Requests to retrieve data.
- **Repositories**: Interfaces for accessing data.
- **Services**: Business logic.

### Frontend (Angular)

- **Components**: Reactive components.
- **Services**: Services for API calls.
- **Modules**: Angular modules for organizing components.
- **Layout**: Navigation bar components.
- **Shared**: Shared components and services.

## API

### Endpoints

- **GET /api/patients**: Retrieve the list of patients.
- **POST /api/patients**: Create a new patient.
- **GET /api/patients/{id}**: Retrieve a patient's details.
- **PUT /api/patients/{id}**: Update a patient.
- **DELETE /api/patients/{id}**: Delete a patient.


## API Project Structure

PFF
├───.dockerignore
├───docker-compose.yml
├───Dockerfile
├───PFF.slnx
│
├───PFF.Api
│   ├───appsettings.json
│   ├───appsettings.Development.json
│   ├───PFF.Api.csproj
│   ├───PFF.Api.http
│   ├───Program.cs
│   │
│   ├───Controllers
│   │   ├───AuthController.cs
│   │   ├───PathologyController.cs
│   │   ├───PatientAddressController.cs
│   │   └───PatientController.cs
│   │
│   ├───Dtos
│   │   ├───CreatePathologyDto.cs
│   │   ├───CreatePatientAddressDto.cs
│   │   ├───CreatePatientDto.cs
│   │   ├───RefreshTokenDto.cs
│   │   ├───TokenPairDto.cs
│   │   ├───UpdatePatientAddressDto.cs
│   │   ├───UpdatePatientDto.cs
│   │   └───UserDto.cs
│   │
│   ├───Infrastructure
│   │   ├───ControllerBaseExtensions.cs
│   │   ├───ITokenService.cs
│   │   └───TokenService.cs
│   │
│   └───Properties
│       └───launchSettings.json
│
├───PFF.Domain
│   ├───ApplicationDbContext.cs
│   ├───PFF.Domain.csproj
│   │
│   ├───Commands
│   │   ├───Pathology
│   │   │   └───CreatePathologyCommand.cs
│   │   │
│   │   └───Patient
│   │       ├───CreatePatientCommand.cs
│   │       ├───DeletePatientCommand.cs
│   │       └───UpdatePatientCommand.cs
│   │
│   ├───Configurations
│   │   ├───ChronicTreatmentConfig.cs
│   │   ├───ConsultationConfig.cs
│   │   ├───MedicineConfig.cs
│   │   ├───PathologyConfig.cs
│   │   ├───PatientAddressConfig.cs
│   │   ├───PatientConfig.cs
│   │   ├───PrescriptionConfig.cs
│   │   ├───VitalSignConfig.cs
│   │   └───WorkerConfig.cs
│   │
│   ├───Errors
│   │   ├───ConsultationErrors.cs
│   │   ├───MedicineErrors.cs
│   │   ├───PathologyErrors.cs
│   │   ├───PatientAddressErrors.cs
│   │   ├───PatientErrors.cs
│   │   ├───PrescriptionErrors.cs
│   │   ├───VitalSignErrors.cs
│   │   └───WorkerErrors.cs
│   │
│   ├───Migrations
│   │   ├───20261007090705_InitiatCreate.cs
│   │   ├───20261007090705_InitiatCreate.Designer.cs
│   │   ├───20261007091621_AdjustSSINAndIdNumberLength.cs
│   │   ├───20261007091621_AdjustSSINAndIdNumberLength.Designer.cs
│   │   └───ApplicationDbContextModelSnapshot.cs
│   │
│   ├───Model
│   │   └───Entities
│   │       ├───ChronicTreatment.cs
│   │       ├───Consultation.cs
│   │       ├───Medicine.cs
│   │       ├───Pathology.cs
│   │       ├───Patient.cs
│   │       ├───PatientAddress.cs
│   │       ├───Prescription.cs
│   │       ├───User.cs
│   │       ├───VitalSign.cs
│   │       └───Worker.cs
│   │
│   ├───Queries
│   │   ├───GetPathologiesQuery.cs
│   │   ├───GetPatientDetailsQuery.cs
│   │   └───GetPatientsQuery.cs
│   │
│   ├───Repositories
│   │   ├───IAuthRepository.cs
│   │   ├───IPathologyRepository.cs
│   │   ├───IPatientAddressRepository.cs
│   │   └───IPatientRepository.cs
│   │
│   └───Services
│       ├───AuthService.cs
│       ├───PathologyService.cs
│       ├───PatientAddressService.cs
│       └───PatientService.cs
│
├───PFF.Shared
│   ├───PFF.Shared.csproj
│   │
│   └───Dtos
│       └───LoginDto.cs
│
└───Tools
    ├───PFF.Tools.csproj
    │
    ├───CommandQuerySeparation
    │   ├───ICommandDefinition.cs
    │   ├───ICommandHandler.cs
    │   ├───IQueryDefinition.cs
    │   └───IQueryHandler.cs
    │
    └───Results
        ├───Error.cs
        └───Result.cs


## Angular Project Structure

PFF.Angular
├───angular.json
├───docker-compose.yml
├───dockerfile
├───nginx.conf
├───package.json
├───tsconfig.json
│
└───src
    ├───index.html
    ├───main.ts
    ├───styles.css
    │
    ├───app
    │   ├───app.config.ts
    │   ├───app.css
    │   ├───app.html
    │   ├───app.routes.ts
    │   ├───app.ts
    │   │
    │   ├───core
    │   │   └───services
    │   │       ├───patientAddresses.service.ts
    │   │       └───patients.service.ts
    │   │
    │   ├───features
    │   │   ├───dashboard
    │   │   │   ├───dashboard.css
    │   │   │   ├───dashboard.html
    │   │   │   └───dashboard.ts
    │   │   │
    │   │   └───patients
    │   │       ├───patients.routes.ts
    │   │       │
    │   │       ├───patient-creation
    │   │       │   ├───patient-creation.css
    │   │       │   ├───patient-creation.html
    │   │       │   └───patient-creation.ts
    │   │       │
    │   │       ├───patient-delete
    │   │       │   ├───patient-delete.css
    │   │       │   ├───patient-delete.html
    │   │       │   └───patient-delete.ts
    │   │       │
    │   │       ├───patient-details
    │   │       │   ├───patient-details.css
    │   │       │   ├───patient-details.html
    │   │       │   └───patient-details.ts
    │   │       │
    │   │       ├───patient-update
    │   │       │   ├───patient-update.css
    │   │       │   ├───patient-update.html
    │   │       │   └───patient-update.ts
    │   │       │
    │   │       └───patients-list
    │   │           ├───patients-list.css
    │   │           ├───patients-list.html
    │   │           └───patients-list.ts
    │   │
    │   ├───layout
    │   │   └───nav-bar
    │   │       ├───nav-bar.css
    │   │       ├───nav-bar.html
    │   │       └───nav-bar.ts
    │   │
    │   └───shared
    │       ├───enums
    │       │   ├───consumption-frequency.enum.ts
    │       │   ├───drug-type.enum.ts
    │       │   ├───gender.enum.ts
    │       │   └───residence-status.enum.ts
    │       │
    │       ├───models
    │       │   ├───patient.model.ts
    │       │   └───patientAddress.model.ts
    │       │
    │       ├───pipes
    │       │   ├───consumption-label-pipe.ts
    │       │   ├───drug-label-pipe.ts
    │       │   ├───gender-label-pipe.ts
    │       │   └───residence-label-pipe.ts
    │       │
    │       └───validators
    │           └───legal-age.validator.ts
    │
    └───environments
        ├───environment.development.ts
        └───environment.ts