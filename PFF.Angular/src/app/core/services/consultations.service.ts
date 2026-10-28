import { inject, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CreateConsultationRequest, Consultation, UpdateConsultationRequest } from '../../shared/models/consultation.model';
import { Observable } from 'rxjs';

@Service()
export class ConsultationsService {

    private readonly http: HttpClient = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/consultations`;

    getAll(): Observable<Consultation[]> {
        return this.http.get<Consultation[]>(this.baseUrl);
    }

    getById(id: number): Observable<Consultation> {
        return this.http.get<Consultation>(`${this.baseUrl}/${id}`);
    }

    create(request: CreateConsultationRequest): Observable<Consultation> {
        return this.http.post<Consultation>(this.baseUrl, request);
    }

    update(id: number, request: UpdateConsultationRequest): Observable<Consultation> {
        return this.http.put<Consultation>(`${this.baseUrl}/${id}`, request);
    }

    delete(id: number): Observable<Consultation> {
        return this.http.delete<Consultation>(`${this.baseUrl}/${id}`);
    }

}
