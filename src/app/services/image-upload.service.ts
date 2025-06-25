import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ImageUpload {
  private apiUrl = 'Account/upload'; // Cambia porta/URL se diverso

  constructor(private http: HttpClient) {}

  uploadImage(base64Image: string, fileName: string, contentType: string) {
    const body = {
      base64Image,
      fileName,
      contentType
    };

    return this.http.post(`${environment.apiUrl}${this.apiUrl}`, body);
  }
}
