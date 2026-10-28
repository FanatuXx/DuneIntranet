import { Consultation } from "./consultation.model";
import { Medicine } from "./medicine.model";
import { Patient } from "./patient.model";
import { Worker } from "./worker.model";

export interface Prescription {
    id: number,
    startDate: Date,
    endDate: Date,
    takeFrequency: string,

    worker: Worker,
    patient: Patient,
    consultation: Consultation,
    
    medicines: Medicine[]
}

export interface CreatePrescriptionRequest {
    startDate: Date,
    endDate: Date,
    takeFrequency: string,

    prescriberId: number,
    medicinesIds: number[]
}

export interface UpdatePrescriptionRequest {
    id: number,
    startDate: Date | null,
    endDate: Date | null,
    takeFrequency: string | null,

    prescriberId: number | null,
    medicinesIds: number[] | null
}