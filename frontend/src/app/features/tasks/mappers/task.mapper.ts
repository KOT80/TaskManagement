
import { TaskDto } from '../models/api/task.dto';
import { Task } from '../models/task.model';

export class TaskMapper {
  static toModel(dto: TaskDto): Task {
    return {
      id: dto.id,
      title: dto.title,
      description: dto.description,
      isCompleted: dto.isCompleted,
      createdAt: new Date(dto.createdAt),
    };
  }

  static toModelList(dtos: TaskDto[]): Task[] {
    return dtos.map((dto) => TaskMapper.toModel(dto));
  }
}
