import { ConsumptionFrequency } from "../enums/consumption-frequency.enum";
import { DrugType } from "../enums/drug-type.enum";
import { Gender } from "../enums/gender.enum";
import { ResidenceStatus } from "../enums/residence-status.enum";

export interface Patient {
    id: number,
    ssin: string | null,
    idNumber: string | null,
    firstName: string | null,
    lastName: string | null, 
    alias: string | null,
    gender: keyof typeof Gender | null,
    birthDate: Date,
    phoneNumber: string | null,
    allergies: string | null,
    isInsured: boolean | null,
    insurance: string | null,
    hasInsuranceCard: boolean | null,
    insuranceCardEndDate: Date | null,
    isAtFedasil: boolean | null,
    income: number | null,
    status: keyof typeof ResidenceStatus | null,
    isWorking: boolean | null,
    drugType: keyof typeof DrugType | null,
    consumptionFrequency: keyof typeof ConsumptionFrequency | null
    registrationDate: Date,
    lastVisit: Date
    patientAddressId: number | null
    street: string | null,
    number: string | null,
    zipCode: string,
    town: string | null,
    country: string | null
}

export interface CreatePatientRequest {
    ssin: string | null,
    idNumber: string | null,
    firstName: string | null,
    lastName: string | null, 
    alias: string | null,
    gender: keyof typeof Gender | null,
    birthDate: Date,
    phoneNumber: string | null,
    allergies: string | null,
    isInsured: boolean | null,
    insurance: string | null,
    hasInsuranceCard: boolean | null,
    insuranceCardEndDate: Date | null,
    isAtFedasil: boolean | null,
    income: number | null,
    status: keyof typeof ResidenceStatus | null,
    isWorking: boolean | null,
    drugType: keyof typeof DrugType | null,
    consumptionFrequency: keyof typeof ConsumptionFrequency | null
    street: string | null,
    number: string | null,
    zipCode: string,
    town: string | null,
    country: string | null
}

export interface UpdatePatientRequest {
    ssin: string | null,
    idNumber: string | null,
    firstName: string | null,
    lastName: string | null, 
    alias: string | null,
    gender: keyof typeof Gender | null,
    birthDate: Date | null,
    phoneNumber: string | null,
    allergies: string | null,
    isInsured: boolean | null,
    insurance: string | null,
    hasInsuranceCard: boolean | null,
    insuranceCardEndDate: Date | null,
    isAtFedasil: boolean | null,
    income: number | null,
    status: keyof typeof ResidenceStatus | null,
    isWorking: boolean | null,
    drugType: keyof typeof DrugType | null,
    consumptionFrequency: keyof typeof ConsumptionFrequency | null
}

