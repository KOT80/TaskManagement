
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { TaskDto } from '../models/api/task.dto';
import { CreateTaskRequestDto } from '../models/api/create-task-request.dto';
import { UpdateTaskRequestDto } from '../models/api/update-task-request.dto';

@Injectable({
  providedIn: 'root',
})
export class TaskApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/tasks`;

  getAll(isCompleted?: boolean): Observable<TaskDto[]> {
    const url =
      isCompleted === undefined
        ? this.apiUrl
        : `${this.apiUrl}?isCompleted=${isCompleted}`;

    return this.http.get<TaskDto[]>(url);
  }

  getById(id: string): Observable<TaskDto> {
    return this.http.get<TaskDto>(`${this.apiUrl}/${id}`);
  }

  create(request: CreateTaskRequestDto): Observable<string> {
    return this.http.post<string>(this.apiUrl, request);
  }

  update(
    id: string,
    request: UpdateTaskRequestDto,
  ): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  setCompletion(
    id: string,
    isCompleted: boolean,
  ): Observable<void> {
    return this.http.patch<void>(
      `${this.apiUrl}/${id}/completion`,
      { isCompleted },
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
