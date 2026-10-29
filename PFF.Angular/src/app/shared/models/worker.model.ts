import { Gender } from "../enums/gender.enum";
import { Consultation } from "./consultation.model";
import { Prescription } from "./prescription.model";

export interface Worker {
    id: number,
    ssin: string,
    firstName: string,
    lastName: string, 
    gender: keyof typeof Gender,
    birthDate: Date,
    email: string,
    phoneNumber: string,
    occupation: string,

    street: string,
    number: string,
    zipCode: string,
    town: string,
    country: string,

    consultations: Consultation[],
    prescriptions: Prescription[]
}

export interface CreateWorkerRequest {
    ssin: string,
    firstName: string,
    lastName: string, 
    gender: keyof typeof Gender,
    birthDate: Date,
    email: string,
    phoneNumber: string,
    occupation: string,

    street: string,
    number: string,
    zipCode: string,
    town: string,
    country: string
}

export interface UpdateWorkerRequest {
    id: number,
    ssin: string | null,
    firstName: string | null,
    lastName: string | null, 
    gender: keyof typeof Gender | null,
    birthDate: Date | null,
    email: string | null,
    phoneNumber: string | null,
    occupation: string | null,

    street: string | null,
    number: string | null,
    zipCode: string | null,
    town: string | null,
    country: string | null
}