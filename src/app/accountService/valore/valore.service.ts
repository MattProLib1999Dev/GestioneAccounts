import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { postValoriOutput } from '../../models/valori/postValoriOutput';
import { putValori } from '../../models/valori/putValori';


@Injectable({
  providedIn: 'root'
})
export class ValoreService {

  appConfig: any;
  rootUrl?:string = "http://localhost:5082";

  constructor(private httpClient: HttpClient) {}

    getValori(): Observable<any> {
      return this.httpClient.get<any>(`${this.rootUrl}/api/Valori`);
    }

    postValori(valori: postValoriOutput): Observable<postValoriOutput[]> {
      return this.httpClient.post<postValoriOutput[]>(`${this.rootUrl}/api/Valori/Create`, valori);
    }

    putValori(valoriId: number, putAccount:putValori): Observable<putValori> {
      return this.httpClient.put<putValori>(`${this.rootUrl}/api/Valori/${valoriId}`, putAccount);
    }

    getByIdValore(valoriId: number): Observable<any> {
      return this.httpClient.get<any>(`${this.rootUrl}/api/Valori/${valoriId}`);
    }

    deleteValore(valoriId: number): Observable<any> {
      return this.httpClient.delete<any>(`${this.rootUrl}/api/Valori/Delete/${valoriId}`);
    }



 }
