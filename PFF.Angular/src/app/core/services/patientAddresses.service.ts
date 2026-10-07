import { HttpClient } from "@angular/common/http";
import { inject, Service } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { CreatePatientAddressRequest, PatientAddress, UpdatePatientAddressRequest } from "../../shared/models/patientAddress.model";

@Service()
export class PatientAddressesService {

    private readonly http: HttpClient = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/patients`;

    getAll(): Observable<PatientAddress[]> {
        return this.http.get<PatientAddress[]>(this.baseUrl);
    }

    getById(id: number): Observable<PatientAddress> {
        return this.http.get<PatientAddress>(`${this.baseUrl}/${id}`);
    }

    create(request: CreatePatientAddressRequest): Observable<PatientAddress> {
        return this.http.post<PatientAddress>(this.baseUrl, request);
    }

    update(id: number, request: UpdatePatientAddressRequest): Observable<PatientAddress> {
        return this.http.put<PatientAddress>(`${this.baseUrl}/${id}`, request);
    }

    delete(id: number): Observable<PatientAddress> {
        return this.http.delete<PatientAddress>(`${this.baseUrl}/${id}`);
    }
}