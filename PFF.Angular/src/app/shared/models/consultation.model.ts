import { Pathology } from "./pathology.model";
import { Patient } from "./patient.model";
import { CreatePrescriptionRequest, Prescription, UpdatePrescriptionRequest } from "./prescription.model";
import { CreateVitalSignRequest, UpdateVitalSignRequest, VitalSign } from "./vital-sign.model";
import { Worker } from "./worker.model";

export interface Consultation {
    id: number,
    motive: string,
    subjective: string | null,
    objective: string | null,
    assessment: string | null,
    plan: string | null,
    description: string | null,
    date: Date,

    patient: Patient,
    workers: Worker[],
    prescriptions: Prescription[]  | null,
    vitalsigns: VitalSign[] | null,
    pathologies: Pathology[]
}

export interface CreateConsultationRequest {
    motive: string,
    subjective: string | null,
    objective: string | null,
    assessment: string | null,
    plan: string | null,
    description: string | null,
    date: Date,
    patientId: number,

    workersIds: number[],
    pathologiesIds: number[],

    prescriptions: CreatePrescriptionRequest[] | null,
    vitalSigns: CreateVitalSignRequest[] | null,
}

export interface UpdateConsultationRequest {
    id: number
    motive: string,
    subjective: string | null,
    objective: string | null,
    assessment: string | null,
    plan: string | null,
    description: string | null,
    date: Date | null,
    patientId: number | null,

    workersIds: number[] | null,
    pathologiesIds: number[] | null,

    prescriptionsToUpdate: UpdatePrescriptionRequest[] | null,
    prescriptionsToCreate: CreatePrescriptionRequest[] | null,
    vitalSignsToUpdate: UpdateVitalSignRequest[] | null,
    vitalSignsToCreate: CreateVitalSignRequest[] | null,
}