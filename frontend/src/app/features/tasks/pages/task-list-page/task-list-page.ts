
import { DatePipe } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { CreateTaskRequestDto } from '../../models/api/create-task-request.dto';
import { UpdateTaskRequestDto } from '../../models/api/update-task-request.dto';
import { Task } from '../../models/task.model';
import { TaskStore } from '../../state/task.store';

@Component({
  selector: 'app-task-list-page',
  standalone: true,
  imports: [DatePipe, FormsModule],
  templateUrl: './task-list-page.html',
  styleUrl: './task-list-page.scss',
})
export class TaskListPage implements OnInit {
  readonly store = inject(TaskStore);

  isCreateFormVisible = false;
  editingTaskId: string | null = null;

  title = '';
  description = '';

  ngOnInit(): void {
    this.store.loadTasks();
  }

  showCreateForm(): void {
    if (this.store.operationInProgress()) {
      return;
    }

    this.editingTaskId = null;
    this.resetForm();
    this.isCreateFormVisible = true;
  }


  hideCreateForm(): void {
    this.isCreateFormVisible = false;
    this.editingTaskId = null;
    this.resetForm();
  }


  createTask(): void {
    const normalizedTitle = this.title.trim();

    if (!normalizedTitle || this.store.operationInProgress()) {
      return;
    }

    const request: CreateTaskRequestDto = {
      title: normalizedTitle,
      description: this.description.trim() || null,
    };

    this.store.createTask(request, () => {
      this.hideCreateForm();
    });
  }

  editTask(task: Task): void {
    if (this.store.operationInProgress()) {
      return;
    }

    this.editingTaskId = task.id;
    this.title = task.title;
    this.description = task.description ?? '';
    this.isCreateFormVisible = true;
  }

  updateTask(): void {
    if (!this.editingTaskId || this.store.operationInProgress()) {
      return;
    }

    const normalizedTitle = this.title.trim();

    if (!normalizedTitle) {
      return;
    }

    const request: UpdateTaskRequestDto = {
      title: normalizedTitle,
      description: this.description.trim() || null,
    };

    this.store.updateTask(this.editingTaskId, request, () => {
      this.hideCreateForm();
    });
  }

  deleteTask(id: string): void {
    if (this.store.operationInProgress()) {
      return;
    }

    const confirmed = window.confirm(
      'Вы действительно хотите удалить эту задачу?',
    );

    if (!confirmed) {
      return;
    }

    this.store.deleteTask(id);
  }

  private resetForm(): void {
    this.title = '';
    this.description = '';
  }
}
