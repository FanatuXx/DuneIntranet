import { ConsumptionFrequency } from "../enum/consumption-frequency.enum";
import { DrugType } from "../enum/drug-type.enum";
import { Gender } from "../enum/gender.enum";
import { ResidenceStatus } from "../enum/residence-status.enum";

export interface Patient {
    id: number,
    ssin: string | null,
    idNumber: string | null,
    firstName: string | null,
    lastName: string | null, 
    alias: string | null,
    gender: Gender | null,
    birthDate: Date,
    phoneNumber: string | null,
    allergies: string | null,
    isInsured: boolean | null,
    insurance: string | null,
    hasInsuranceCard: boolean | null,
    insuranceCardEndDate: Date | null,
    isAtFedasil: boolean | null,
    income: number | null,
    status: ResidenceStatus | null,
    isWorking: boolean | null,
    drugType: DrugType | null,
    consumptionFrequency: ConsumptionFrequency | null
    registrationDate: Date,
    lastVisit: Date
    patientAddressId: number | null
}

export interface CreatePatientRequest {
    ssin: string | null,
    idNumber: string | null,
    firstName: string | null,
    lastName: string | null, 
    alias: string | null,
    gender: Gender | null,
    birthDate: Date,
    phoneNumber: string | null,
    allergies: string | null,
    isInsured: boolean | null,
    insurance: string | null,
    hasInsuranceCard: boolean | null,
    insuranceCardEndDate: Date | null,
    isAtFedasil: boolean | null,
    income: number | null,
    status: ResidenceStatus | null,
    isWorking: boolean | null,
    drugType: DrugType | null,
    consumptionFrequency: ConsumptionFrequency | null
}


