import { Gender } from "../enums/gender.enum";
import { ResidenceStatus } from "../enums/residence-status.enum";
import { Consumption } from "./consumption.model";
import { PatientAddress } from "./patient-address.model";

export interface Patient {
    id: number,
    ssin: string | null,
    idNumber: string | null,
    firstName: string | null,
    lastName: string | null, 
    alias: string,
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
    registrationDate: Date,
    lastVisit: Date,

    patientAddress: PatientAddress,

    consumptions: Consumption[] | null
}

export interface CreatePatientRequest {
    ssin: string | null,
    idNumber: string | null,
    firstName: string | null,
    lastName: string | null, 
    alias: string,
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
    
    patientAddress: CreatePatientRequest

    consumptions: Consumption[] | null
}

export interface UpdatePatientRequest {
    id: number,
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

    patientAddress: UpdatePatientRequest | null,

    consumptions: Consumption[] | null
}

