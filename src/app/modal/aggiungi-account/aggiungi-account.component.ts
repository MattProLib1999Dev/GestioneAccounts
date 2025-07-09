import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { AccountService } from '../../accountService/account.service';
import { PostAccounts } from '../../models/PostAccounts';

@Component({
  selector: 'aggiungi-account',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgIf, NgFor],
  providers: [],
  templateUrl: './aggiungi-account.component.html',
  styleUrls: ['./aggiungi-account.component.css']
})
export class AccountComponent implements OnInit {
  accountForm!: FormGroup;
  valoriForm!: FormGroup;
  accounts: any[] = [];
  sortedAccounts: any[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient, private accountService: AccountService) {}

  ngOnInit(): void {
    this.accountForm = this.fb.group({
      nome: ['', Validators.required],
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      valoreString: [''],
      voce: [''],
      dataCreazione: [''],
      passwordHash: [''],
      accessFailedCount: [0, Validators.required],
      emailConfirmed: [false],
      phoneNumberConfirmed: [false],
      twoFactorEnabled: [false]
    });

    this.valoriForm = this.fb.group({
      nomeValore: [''],
      dataCreazioneValore: ['', Validators.required],
      account: [''],
      valoreNumerico: [0],
      descrizione: [''],
      valoreString: [''],
      voce: ['']
    });

    this.getAccounts();
  }

  getAccounts() {
    this.http.get<any[]>('/api/accounts').subscribe(data => {
      this.accounts = data;
      this.sortedAccounts = [...this.accounts];
    });
  }

  sortAccountsByName() {
    this.sortedAccounts = [...this.accounts].sort((a, b) => a.nome.localeCompare(b.nome));
  }

  addAccount() {
    const now = new Date().toISOString();

    const valore = {
      accountId: "string", // puoi anche usare "" se non hai l’id
      nome: this.valoriForm.value.nomeValore,
      descrizione: this.valoriForm.value.descrizione,
      valoreNumerico: this.valoriForm.value.valoreNumerico,
      dataCreazione: new Date(this.valoriForm.value.dataCreazioneValore).toISOString(),
      valoreStr: this.valoriForm.value.valoreString,
      voce: this.valoriForm.value.voce
    };

    const accountData: PostAccounts = {
      userName: this.accountForm.value.userName,
      normalizedUserName: this.accountForm.value.userName.toUpperCase(),
      email: this.accountForm.value.email,
      normalizedEmail: this.accountForm.value.email.toUpperCase(),
      emailConfirmed: this.accountForm.value.emailConfirmed,
      passwordHash: this.accountForm.value.passwordHash,
      securityStamp: "", // se il backend lo imposta puoi lasciare vuoto
      concurrencyStamp: "",
      phoneNumber: this.accountForm.value.phoneNumber,
      phoneNumberConfirmed: this.accountForm.value.phoneNumberConfirmed,
      twoFactorEnabled: this.accountForm.value.twoFactorEnabled,
      lockoutEnd: now,
      lockoutEnabled: true,
      accessFailedCount: this.accountForm.value.accessFailedCount || 0,
      valori: [valore],
      nome: this.accountForm.value.nome,
      voce: this.valoriForm.value.voce,
      valoreString: this.valoriForm.value.valoreString,
      dataCreazione: now
    };

    console.log('Payload inviato al backend:', JSON.stringify(accountData, null, 2));

    this.accountService.createAccount(accountData).subscribe({
      next: res => {
        console.log('Account creato:', res);
      },
      error: err => {
        console.error('Errore nella creazione dell’account:', err);
      }
    });
  }

}
