import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PutAccount } from '../models/PutAccount';
import { getAccount, getAccounts } from '../models/getAccount';
import { search } from '../models/search';
import { PostAccounts } from '../models/PostAccounts';
import { Account } from '../modal/account/account.component';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private rootUrl: string = 'https://localhost:7045/api/Account';  // Modifica con l'URL corretto dell'API

  constructor(private httpClient: HttpClient) { }

  // Metodo per creare un account
  createAccount(account: PostAccounts): Observable<PostAccounts> {
    return this.httpClient.post<PostAccounts>(`${this.rootUrl}/create`, account);
  }

  putAccount(accountId: number, putAccount:PutAccount): Observable<PutAccount> {
    return this.httpClient.put<PutAccount>(`${this.rootUrl}/${accountId}`, putAccount);
  }

  getAccount(): Observable<getAccounts> {
    return this.httpClient.get<getAccounts>(`${this.rootUrl}/all`);
  }

  getByIdAccount(idUtente: number): Observable<any> {
    return this.httpClient.get<any>(`${this.rootUrl}/${idUtente}`);
  }

  deleteAccount(idUtente: number): Observable<any> {
    return this.httpClient.delete<any>(`${this.rootUrl}/Delete/${idUtente}`);
  }

  getOrderByName(): Observable<any> {
    return this.httpClient.get(`${this.rootUrl}/orderByName`);
  }

  search(search: { nome: string; dataCreazione: string; valoreString: string }): Observable<any> {
    const params = new HttpParams()
      .set('nome', search.nome || '')
      .set('dataCreazione', search.dataCreazione || '')
      .set('valoreString', search.valoreString || '');

    return this.httpClient.get(`${this.rootUrl}/search/`, { params });
  }

}


