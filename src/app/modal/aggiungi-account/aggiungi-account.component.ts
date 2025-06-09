import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../accountService/account.service';
import { PostAccounts } from '../../models/PostAccounts';
import { getAccount } from '../../models/getAccount';
import { Account } from '../account/account.component';

@Component({
  selector: 'app-aggiungi-account',
  standalone: true, // Abilita il componente standalone
  templateUrl: './aggiungi-account.component.html',
  styleUrls: ['./aggiungi-account.component.css'],
  imports: [HttpClientModule, FormsModule, CommonModule, ReactiveFormsModule], // Importiamo HttpClientModule direttamente qui
})
export class AggiungiAccountComponent {
  accounts!: getAccount;
  errorMessage: string | null = 'Inserisci un account';
  successMessage: string | null = 'Account aggiunto con successo!';
  selectedValue: any = null; // Inizializza selectedValue a null
  accountForm: FormGroup = new FormGroup({}); // Inizializza accountForm come un nuovo FormGroup
  value: any;
  valoriArray: object[] = []; // Inizializza valoriArray come un array vuoto
  voce!: string | number | null;
  valoreStr!: string | number | null;
  account!: string | number | null;
  dataCreazione!: string | number | null;
  nome!: string | number | null;
  sortedAccounts: getAccount = []; // Inizializza sortedAccounts come un array vuoto

  constructor(
    private http: HttpClient,
    private accountService: AccountService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    // Inizializza il form con FormBuilder
    this.accountForm = this.fb.group({
      nome: ['', Validators.required],
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: [''],
      valoreString: [''],
      voce: [''],
      dataCreazione: ['', Validators.required],
      passwordHash: [''],
      accessFailedCount: [0],
      emailConfirmed: [false],
      phoneNumberConfirmed: [false],
      twoFactorEnabled: [false]
    });


    this.loadAccounts();
    console.log(this.accounts); // Log accounts array to see its structure
  }

  loadAccounts(): void {
    this.accountService.getAccount().subscribe(
      (data: any) => {
        this.accounts = data; // Assegna i dati ricevuti alla variabile accounts
        console.log(this.accounts); // Log accounts array to see its structure
        this.errorMessage = null; // Reset error message on successful load
        this.successMessage = null; // Reset success message on successful load
      },
      (error: Error) => {
        console.error('Error loading accounts:', error);
        this.errorMessage = 'Failed to load accounts.';
      }
    );
  }

  addAccount(account: PostAccounts): void {
    const payload = {
      ...account,
      dataCreazione: new Date(account.dataCreazione).toISOString() // normalize datetime
    };

    console.log(payload); // inspect structure

    this.accountService.createAccount(payload).subscribe(
      (response: PostAccounts) => {
        console.log(response);
        this.successMessage = 'Account created successfully!';
        this.errorMessage = null;
      },
      (error: Error) => {
        alert('An error occurred while creating the account');
        console.error(error);
      }
    );
  }


  deleteAccount(accountId: number): void {
    this.accountService.deleteAccount(accountId).subscribe(
      (response: any) => {
        this.successMessage = 'Account deleted successfully!';
        this.errorMessage = null;
        this.loadAccounts(); // Ricarica gli account dopo la cancellazione
      },
      (error: Error) => {
        console.error('Error deleting account:', error);
        this.errorMessage = 'Failed to delete account.';
        this.successMessage = null;
      }
    );
  }

  sortAccountsByName(): void {
    this.accountService.getOrderByName().subscribe(
      (data: any) => {
        this.sortedAccounts = data || data;
        this.errorMessage = null;
        this.successMessage = null;
        console.log('Accounts sorted by name:', this.accounts);
      },
      (error: Error) => {
        console.error('Errore durante l\'ordinamento:', error);
        this.errorMessage = 'Errore durante l\'ordinamento.';
      }
    );
  }


}
