export interface PatientAddress {
    id: number,
    street: string | null,
    number: string |null,
    zipCode: number,
    town: string | null,
    country: string | null
}

export interface CreatePatientAddressRequest {
    street: string | null,
    number: string |null,
    zipCode: number,
    town: string | null,
    country: string | null
}

export interface UpdatePatientAddressRequest {
    street: string | null,
    number: string |null,
    zipCode: number,
    town: string | null,
    country: string | null
}

