import { inject, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CreateWorkerRequest, Worker, UpdateWorkerRequest } from '../../shared/models/worker.model';
import { Observable } from 'rxjs';

@Service()
export class WorkersService {

    private readonly http: HttpClient = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/workers`;

    getAll(): Observable<Worker[]> {
        return this.http.get<Worker[]>(this.baseUrl);
    }

    getById(id: number): Observable<Worker> {
        return this.http.get<Worker>(`${this.baseUrl}/${id}`);
    }

    create(request: CreateWorkerRequest): Observable<Worker> {
        return this.http.post<Worker>(this.baseUrl, request);
    }

    update(id: number, request: UpdateWorkerRequest): Observable<Worker> {
        return this.http.put<Worker>(`${this.baseUrl}/${id}`, request);
    }

    delete(id: number): Observable<Worker> {
        return this.http.delete<Worker>(`${this.baseUrl}/${id}`);
    }

}