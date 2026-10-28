import { ChronicTreatment } from "./chronic-treatment.model";
import { Prescription } from "./prescription.model";

export interface Medicine {
    id: number,
    name: string,
    howToTake: string,

    prescriptions: Prescription[] | null,
    chronicTreatments: ChronicTreatment[] | null
}

export interface CreateMedicineRequest {
    name: string,
    howToTake: string
}

export interface UpdateMedicineRequest {
    id: number,
    name: string | null,
    howToTake: string | null
}




