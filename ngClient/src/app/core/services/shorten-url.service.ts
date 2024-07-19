import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment.development";
import { HttpClient, HttpParams } from "@angular/common/http";
import { PaginationParams } from "../models/PaginationParams";
import { ShortenUrlRequest } from "../models/ShortenUrlRequest";
import { ShortenUrl, ShortenUrlResponse } from "../models/ShortenUrlResponse";
import { Observable, Subject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ShortenUrlService{
  baseUrl = environment.apiUrl + 'shortenUrl/';
  refreshData = new Subject<void>;

  constructor(private http: HttpClient) {
  }

  getShortenUrls(params: PaginationParams): Observable<ShortenUrlResponse> {
    let httpParams = new HttpParams().set('page', params.page);
    httpParams = httpParams.append('pageSize', params.pageSize);

    return this.http.get<ShortenUrlResponse>(this.baseUrl + 'getAll/', {params: httpParams});
  }

  getShortenUrl(id: number) {
    return this.http.get<ShortenUrl>(this.baseUrl + 'getUrl/' + id);
  }

  createShortenUrl(model: ShortenUrlRequest) {
    return this.http.post<ShortenUrl>(this.baseUrl, model);
  }

  deleteShortenUrl(id: number) {
    return this.http.delete<void>(this.baseUrl + 'delete/' + id);
  }

  deleteAllShortenUrls() {
    return this.http.delete<void>(this.baseUrl + 'deleteAll');
  }

}