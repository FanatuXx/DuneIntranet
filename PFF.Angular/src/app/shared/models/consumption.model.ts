import { ConsumptionFrequency } from "../enums/consumption-frequency.enum";
import { DrugType } from "../enums/drug-type.enum";
import { Patient } from "./patient.model";

export interface Consumption {
    id: number,
    drugType: keyof typeof DrugType,
    frequency: keyof typeof ConsumptionFrequency,
    patient: Patient
}

export interface CreateConsumptionRequest {
    drugType: keyof typeof DrugType,
    frequency: keyof typeof ConsumptionFrequency,
}

export interface UpdateConsumptionRequest {
    id: number,
    drugType: keyof typeof DrugType | null,
    frequency: keyof typeof ConsumptionFrequency | null,
}