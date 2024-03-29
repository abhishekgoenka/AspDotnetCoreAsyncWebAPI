import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/internal/Observable";
import { environment } from "src/environments/environment";

export abstract class BaseService {
  constructor(protected http: HttpClient) {}

  abstract getData<ApiResult>(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string,
    filterQuery: string
  ): Observable<ApiResult>;
  abstract get<T>(id: number): Observable<T>;
  abstract put<T>(item: T): Observable<T>;
  abstract post<T>(item: T): Observable<T>;

  get apiURL(): string {
    return environment.apiUrl
  }
}

export interface ApiResult<T> {
  data: T[];
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  sortColumn: string;
  sortOrder: string;
  filterColumn: string;
  filterQuery: string;
}
