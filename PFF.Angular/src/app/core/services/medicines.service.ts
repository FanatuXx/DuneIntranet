import { inject, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CreateMedicineRequest, Medicine, UpdateMedicineRequest } from '../../shared/models/medicine.model';
import { Observable } from 'rxjs';

@Service()
export class MedicinesService {

    private readonly http: HttpClient = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/medicines`;

    getAll(): Observable<Medicine[]> {
        return this.http.get<Medicine[]>(this.baseUrl);
    }

    getById(id: number): Observable<Medicine> {
        return this.http.get<Medicine>(`${this.baseUrl}/${id}`);
    }

    create(request: CreateMedicineRequest): Observable<Medicine> {
        return this.http.post<Medicine>(this.baseUrl, request);
    }

    update(id: number, request: UpdateMedicineRequest): Observable<Medicine> {
        return this.http.put<Medicine>(`${this.baseUrl}/${id}`, request);
    }

    delete(id: number): Observable<Medicine> {
        return this.http.delete<Medicine>(`${this.baseUrl}/${id}`);
    }

}