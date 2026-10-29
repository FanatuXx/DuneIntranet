import { inject, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CreatePathologyRequest, Pathology, UpdatePathologyRequest } from '../../shared/models/pathology.model';
import { Observable } from 'rxjs';

@Service()
export class PathologiesService {

    private readonly http: HttpClient = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/pathologies`;

    getAll(): Observable<Pathology[]> {
        return this.http.get<Pathology[]>(this.baseUrl);
    }

    getById(id: number): Observable<Pathology> {
        return this.http.get<Pathology>(`${this.baseUrl}/${id}`);
    }

    create(request: CreatePathologyRequest): Observable<Pathology> {
        return this.http.post<Pathology>(this.baseUrl, request);
    }

    update(id: number, request: UpdatePathologyRequest): Observable<Pathology> {
        return this.http.put<Pathology>(`${this.baseUrl}/${id}`, request);
    }

    delete(id: number): Observable<Pathology> {
        return this.http.delete<Pathology>(`${this.baseUrl}/${id}`);
    }

}