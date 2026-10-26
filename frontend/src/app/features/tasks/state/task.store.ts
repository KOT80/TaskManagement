
import { Injectable, computed, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';

import { CreateTaskRequestDto } from '../models/api/create-task-request.dto';
import { UpdateTaskRequestDto } from '../models/api/update-task-request.dto';
import { Task } from '../models/task.model';
import { TaskMapper } from '../mappers/task.mapper';
import { TaskApiService } from '../services/task-api.service';
import { getApiErrorMessage } from '../../../core/errors/api-error.util';

@Injectable({
  providedIn: 'root',
})
export class TaskStore {
  private readonly api = inject(TaskApiService);

  private readonly tasksState = signal<Task[]>([]);
  private readonly loadingState = signal(false);
  private readonly errorState = signal<string | null>(null);
  private readonly filterState = signal<boolean | undefined>(undefined);

  private readonly operationInProgressState = signal(false);
  private readonly operationErrorState = signal<string | null>(null);

  readonly tasks = computed(() => this.tasksState());
  readonly loading = computed(() => this.loadingState());
  readonly error = computed(() => this.errorState());
  readonly filter = computed(() => this.filterState());

  readonly operationInProgress = computed(
    () => this.operationInProgressState(),
  );
  readonly operationError = computed(() => this.operationErrorState());

  loadTasks(): void {
    this.loadingState.set(true);
    this.errorState.set(null);

    this.api
      .getAll(this.filterState())
      .pipe(finalize(() => this.loadingState.set(false)))
      .subscribe({
        next: (dtos) => {
          this.tasksState.set(TaskMapper.toModelList(dtos));
        },
        error: (error: unknown) => {
          this.errorState.set(
            getApiErrorMessage(
              error,
              'Не удалось загрузить задачи. Попробуйте ещё раз.',
            ),
          );
        },
      });
  }

  setFilter(value: boolean | undefined): void {
    this.filterState.set(value);
    this.loadTasks();
  }

  createTask(
    request: CreateTaskRequestDto,
    onSuccess?: () => void,
  ): void {
    if (this.operationInProgressState()) {
      return;
    }

    this.operationInProgressState.set(true);
    this.operationErrorState.set(null);

    this.api
      .create(request)
      .pipe(finalize(() => this.operationInProgressState.set(false)))
      .subscribe({
        next: () => {
          this.loadTasks();
          onSuccess?.();
        },
        error: (error: unknown) => {
          this.operationErrorState.set(
            getApiErrorMessage(
              error,
              'Не удалось создать задачу. Попробуйте ещё раз.',
            ),
          );
        },
      });
  }

  updateTask(
    id: string,
    request: UpdateTaskRequestDto,
    onSuccess?: () => void,
  ): void {
    if (this.operationInProgressState()) {
      return;
    }

    this.operationInProgressState.set(true);
    this.operationErrorState.set(null);

    this.api
      .update(id, request)
      .pipe(finalize(() => this.operationInProgressState.set(false)))
      .subscribe({
        next: () => {
          this.loadTasks();
          onSuccess?.();
        },
        error: (error: unknown) => {
          this.operationErrorState.set(
            getApiErrorMessage(
              error,
              'Не удалось сохранить изменения. Попробуйте ещё раз.',
            ),
          );
        },
      });
  }

  setTaskCompletion(id: string, isCompleted: boolean): void {
    if (this.operationInProgressState()) {
      return;
    }

    this.operationInProgressState.set(true);
    this.operationErrorState.set(null);

    this.api
      .setCompletion(id, isCompleted)
      .pipe(finalize(() => this.operationInProgressState.set(false)))
      .subscribe({
        next: () => {
          this.loadTasks();
        },
        error: (error: unknown) => {
          this.operationErrorState.set(
            getApiErrorMessage(
              error,
              'Не удалось изменить статус задачи. Попробуйте ещё раз.',
            ),
          );
        },
      });
  }

  deleteTask(id: string, onSuccess?: () => void): void {
    if (this.operationInProgressState()) {
      return;
    }

    this.operationInProgressState.set(true);
    this.operationErrorState.set(null);

    this.api
      .delete(id)
      .pipe(finalize(() => this.operationInProgressState.set(false)))
      .subscribe({
        next: () => {
          this.loadTasks();
          onSuccess?.();
        },
        error: (error: unknown) => {
          this.operationErrorState.set(
            getApiErrorMessage(
              error,
              'Не удалось удалить задачу. Попробуйте ещё раз.',
            ),
          );
        },
      });
  }
}
