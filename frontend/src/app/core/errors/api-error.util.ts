
import { HttpErrorResponse } from '@angular/common/http';

type ProblemDetails = {
  detail?: unknown;
  errors?: unknown;
};

const DEFAULT_ERROR = 'Произошла ошибка. Попробуйте ещё раз.';
const MAX_MESSAGE_LENGTH = 300;
const MAX_VALIDATION_MESSAGES = 5;

function isRecord(value: unknown): value is Record<string, unknown> {
  return (
    typeof value === 'object' &&
    value !== null &&
    !Array.isArray(value)
  );
}

function isSafeUserMessage(value: unknown): value is string {
  if (typeof value !== 'string') {
    return false;
  }

  const message = value.trim();

  return (
    message.length > 0 &&
    message.length <= MAX_MESSAGE_LENGTH &&
    !/stack trace|exception| at [\w.$]+ \(|\b(system|inner exception)\b/i.test(
      message,
    )
  );
}

function getValidationMessages(errors: unknown): string[] {
  if (!isRecord(errors)) {
    return [];
  }

  return Object.values(errors)
    .flatMap((value) => {
      if (typeof value === 'string') {
        return [value];
      }

      if (Array.isArray(value)) {
        return value.filter(
          (item): item is string => typeof item === 'string',
        );
      }

      return [];
    })
    .map((message) => message.trim())
    .filter(isSafeUserMessage)
    .slice(0, MAX_VALIDATION_MESSAGES);
}

export function getApiErrorMessage(
  error: unknown,
  fallback = DEFAULT_ERROR,
): string {
  if (!(error instanceof HttpErrorResponse)) {
    return fallback;
  }

  if (error.status === 0) {
    return 'Не удалось связаться с сервером. Проверьте подключение и попробуйте ещё раз.';
  }

  const body: ProblemDetails = isRecord(error.error)
    ? error.error
    : {};

  if (error.status >= 500) {
    return 'На сервере произошла ошибка. Попробуйте позже.';
  }

  if (error.status === 401 || error.status === 403) {
    return 'Недостаточно прав для выполнения операции.';
  }

  if ([400, 422].includes(error.status)) {
    const validationMessages = getValidationMessages(body.errors);

    if (validationMessages.length > 0) {
      return validationMessages.join(' ');
    }
  }

  if (
    [400, 404, 409, 422].includes(error.status) &&
    isSafeUserMessage(body.detail)
  ) {
    return body.detail.trim();
  }

  if (error.status === 404) {
    return 'Запрошенная задача не найдена.';
  }

  if (error.status === 400 || error.status === 422) {
    return 'Проверьте введённые данные.';
  }

  if (error.status === 409) {
    return 'Операция конфликтует с текущим состоянием данных.';
  }

  return fallback;
}
