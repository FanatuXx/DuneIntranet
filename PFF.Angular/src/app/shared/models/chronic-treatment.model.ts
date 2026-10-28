import { Medicine } from "./medicine.model";
import { Pathology } from "./pathology.model";
import { Patient } from "./patient.model";

export interface ChronicTreatment {
    id: number,
    dosage: string,
    pathology: Pathology,
    patient: Patient,
    medicines: Medicine[]
}
