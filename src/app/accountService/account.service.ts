import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PutAccount } from '../models/PutAccount';
import { PostAccounts } from '../models/PostAccounts';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private rootUrl: string = 'http://localhost:5082/api/Account';  // Modifica con l'URL corretto dell'API

  constructor(private httpClient: HttpClient) { }

  // Metodo per creare un account
  createAccount(account: PostAccounts): Observable<PostAccounts> {
    return this.httpClient.post<PostAccounts>(`${this.rootUrl}/create`, account);
  }

  putAccount(accountId: number, putAccount:PutAccount): Observable<PutAccount> {
    return this.httpClient.put<PutAccount>(`${this.rootUrl}/${accountId}`, putAccount);
  }

  getAccount(): Observable<any> {
    return this.httpClient.get<any>(`${this.rootUrl}/all`);
  }

  getByIdAccount(idUtente: number): Observable<any> {
    return this.httpClient.get<any>(`${this.rootUrl}/${idUtente}`);
  }

  deleteAccount(idUtente: number): Observable<any> {
    return this.httpClient.delete<any>(`${this.rootUrl}/Delete/${idUtente}`);
  }
}


