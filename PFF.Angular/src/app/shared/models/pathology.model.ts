import { ChronicTreatment } from "./chronic-treatment.model";
import { Consultation } from "./consultation.model";

export interface Pathology {
    id: number,
    name: string,

    consultations: Consultation[],
    chronicTreatments: ChronicTreatment[]
}