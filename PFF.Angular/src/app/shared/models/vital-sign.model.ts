import { Consultation } from "./consultation.model";

export interface VitalSign {
    id: number,
    heartRate: number,
    bloodPressure: string | null,
    temperature: number | null,
    oxygenSaturation: number,
    height: number | null,
    weight: number | null,
    bmi: number,
    bloodSugar: number | null,
    date: Date,

    consultation: Consultation | null
}

export interface CreateVitalSignRequest {
    heartRate: number,
    bloodPressure: string | null,
    temperature: number | null,
    oxygenSaturation: number,
    height: number | null,
    weight: number | null,
    bmi: number | null,
    bloodSugar: number | null,
    date: Date,
}

export interface UpdateVitalSignRequest {
    id: number,
    heartRate: number | null,
    bloodPressure: string | null,
    temperature: number | null,
    oxygenSaturation: number,
    height: number | null,
    weight: number | null,
    bloodSugar: number | null,
    date: Date | null,
}