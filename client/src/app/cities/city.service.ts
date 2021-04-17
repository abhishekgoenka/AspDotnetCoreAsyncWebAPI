import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { BaseService } from "../base.service";

import { City } from "./city";

@Injectable({
  providedIn: "root",
})
export class CityService extends BaseService {
  constructor(http: HttpClient) {
    super(http);
  }

  getData<ApiResult>(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string,
    filterQuery: string
  ): Observable<ApiResult> {
    var url = "api/Cities";
    var params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);
    if (filterQuery) {
      params = params
        .set("filterColumn", filterColumn)
        .set("filterQuery", filterQuery);
    }
    return this.http.get<ApiResult>(url, { params });
  }

  get<City>(id: number): Observable<City> {
    var url = "api/Cities/" + id;
    return this.http.get<City>(url);
  }

  put<City>(item: any): Observable<City> {
    var url = "api/Cities/" + item.id;
    return this.http.put<City>(url, item);
  }
  post<City>(item: City): Observable<City> {
    var url = "api/Cities";
    return this.http.post<City>(url, item);
  }

  getCountries<ApiResult>(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string,
    filterQuery: string
  ): Observable<ApiResult> {
    var url = "api/Countries";
    var params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);
    if (filterQuery) {
      params = params
        .set("filterColumn", filterColumn)
        .set("filterQuery", filterQuery);
    }
    return this.http.get<ApiResult>(url, { params });
  }

  isDupeCity(item: City): Observable<boolean> {
    var url = "api/Cities/IsDupeCity";
    return this.http.post<boolean>(url, item);
  }
}
