import { inject, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CreatePatientRequest, Patient, UpdatePatientRequest } from '../../shared/models/patient.model';
import { Observable } from 'rxjs';

@Service()
export class PatientsService {

    private readonly http: HttpClient = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/patients`;

    getAll(): Observable<Patient[]> {
        return this.http.get<Patient[]>(this.baseUrl);
    }

    getById(id: number): Observable<Patient> {
        return this.http.get<Patient>(`${this.baseUrl}/${id}`);
    }

    create(request: CreatePatientRequest): Observable<Patient> {
        return this.http.post<Patient>(this.baseUrl, request);
    }

    update(id: number, request: UpdatePatientRequest): Observable<Patient> {
        return this.http.put<Patient>(`${this.baseUrl}/${id}`, request);
    }

}
