import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AccountService } from '../../accountService/account.service';
import { PostAccounts } from '../../models/PostAccounts';
import { HttpClient } from '@angular/common/http';
import { Base64Image } from '../../models/base64'; // Assuming you have a base64 string to upload
import { BASE64_IMAGES } from '../../models/base64'; // Assuming you have a predefined set of base64 images
import { ImageUpload } from '../../services/image-upload.service';
import { AccountComponent } from '../aggiungi-account/aggiungi-account.component';
import { TabellaInserimentoOreComponent } from '../../tabella-inserimento-ore/tabella-inserimento-ore.component';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AccountComponent, // Importing the component for adding accounts
    TabellaInserimentoOreComponent
  ],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
})
export class Account implements OnInit {

  formAggiungi!: FormGroup;
  accountadded?: PostAccounts;
  imageUrl?: string;
  base64: Base64Image = BASE64_IMAGES[0]; // Assuming you want to use the first image from the predefined set

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder,
    private router: Router,
    private httpClient: HttpClient,
    private imageUploadService: ImageUpload
  ) {}

  ngOnInit(): void {
    this.formAggiungi = this.formBuilder.group({
      nome: ['Seleziona', Validators.required],
      dataCreazione: ['', Validators.required]
    });
    const fileName = 'matt';
    const contentType = 'jpg';

    const payload = {
      base64Image: this.base64.base64, // Usa il primo Base64
      fileName: 'matt',
      contentType: 'image/png' // Assicurati che corrisponda al mime type corretto
    };

    this.imageUploadService.uploadImage(payload.base64Image, payload.contentType, payload.contentType).subscribe({
      next: (res) => {
        console.log('✅ Immagine caricata con successo:', res);
        this.imageUrl = res.toString(); // Assuming the response contains the image URL
      },
      error: (err) => {
        console.error('❌ Errore nel caricamento immagine:', err);
      }
    });
  }

  onAnimationEnd(): void {
    this.router.navigate(['/aggiungi-account']);
  }
}
