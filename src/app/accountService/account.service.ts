import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PutAccount } from '../models/PutAccount';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private rootUrl: string = 'http://localhost:5000/api/Account';  // Modifica con l'URL corretto dell'API

  constructor(private httpClient: HttpClient) { }

  // Metodo per creare un account
  createAccount(nome: string, valoreString: string, voce: string, valori: { valore: string }[]): Observable<any> {
    // Crea un oggetto FormData per inviare i dati tramite multipart/form-data
    const formData = new FormData();

    // Aggiungi i campi al FormData
    formData.append('nome', nome);
    formData.append('valoreString', valoreString);
    formData.append('voce', voce);

    // Aggiungi la lista di "valori" al FormData (ogni "valore" è un campo separato)
    for (let i = 0; i < valori.length; i++) {
      formData.append(`valori[${i}].valore`, valori[i].valore);
    }

    // Invia la richiesta POST all'API
    return this.httpClient.post<any>(`${this.rootUrl}/create`, formData, {
      headers: new HttpHeaders()
    });
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


