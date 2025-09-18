export interface Response<T> {
  succeeded: boolean;
  message: string;
  errors: string[];
  data: T;
}

export interface PagedResponse<T> {
  succeeded: boolean;
  message: string;
  errors: string[];

  pageNumber: number;
  pageSize: number;
  data: T[];
}

export interface KeyValueResponse<T> extends Response<T> {
  count: number;
}
