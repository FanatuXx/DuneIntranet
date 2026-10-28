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