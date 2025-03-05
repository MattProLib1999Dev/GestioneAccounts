import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PutAccount } from '../models/PutAccount';
import { PostAccounts } from '../models/PostAccounts';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  appConfig: any;
  rootUrl?:string = "http://localhost:5082";

  constructor(private httpClient: HttpClient) {}

    getAccount(): Observable<any> {
      return this.httpClient.get<any>(`${this.rootUrl}/api/Account`);
    }

    postAccount(account: PostAccounts): Observable<PostAccounts[]> {
      return this.httpClient.post<PostAccounts[]>(`${this.rootUrl}/api/Account/Create`, account);
    }

    putAccount(accountId: number, putAccount:PutAccount): Observable<PutAccount> {
      return this.httpClient.put<PutAccount>(`${this.rootUrl}/api/Account/${accountId}`, putAccount);
    }

    getByIdAccount(idUtente: number): Observable<any> {
      return this.httpClient.get<any>(`${this.rootUrl}/api/Account/${idUtente}`);
    }

    deleteAccount(idUtente: number): Observable<any> {
      return this.httpClient.delete<any>(`${this.rootUrl}/api/Account/Delete/${idUtente}`);
    }



 }
