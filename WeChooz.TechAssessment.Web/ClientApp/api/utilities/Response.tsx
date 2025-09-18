import { PagedResponse } from './model/Response';

export function handleResponse(response: any) {
  if (response.data.succeeded != undefined) {
    return response.data;
  } else {
    //TODO show error with message as notification
  }
}

export function handleError(error: any) {
  if (error.response.data.Succeeded != undefined) {
    return error.response.data;
  }

  return error;
}

export function handlePagedResponse<T>(response: any) {
  return response.data as PagedResponse<T>;
}
