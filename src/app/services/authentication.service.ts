import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../models/register';
import { JwtAuth } from '../models/jwtAuth';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Login } from '../models/login';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  registerUrl: string = 'AuthManagment/register';
  loginUrl: string = 'AuthManagment/Login';

  constructor(private http:HttpClient) { }

  public register(user: Register) : Observable<JwtAuth> {
    return this.http.post<JwtAuth>(`${environment.apiUrl}${this.registerUrl}`, user);
  }

  public login(user: Login) : Observable<JwtAuth> {
    return this.http.post<JwtAuth>(`${environment.apiUrl}${this.loginUrl}`, user);
  }
}
