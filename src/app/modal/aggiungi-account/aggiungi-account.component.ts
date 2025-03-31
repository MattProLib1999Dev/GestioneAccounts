import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../accountService/account.service';
import { PostAccounts, Valori } from '../../models/PostAccounts';
import { getAccount } from '../../models/getAccount';



@Component({
  selector: 'app-aggiungi-account',
  standalone: true, // Abilita il componente standalone
  templateUrl: './aggiungi-account.component.html',
  styleUrls: ['./aggiungi-account.component.css'],
  imports: [HttpClientModule, FormsModule, CommonModule, ReactiveFormsModule], // Importiamo HttpClientModule direttamente qui
})
export class AggiungiAccountComponent {
  accounts!: getAccount;
  errorMessage: string | null = "Inserisci un account";
  successMessage: string | null = "Account aggiunto con successo!";

  values: Valori[] = []; // Inizializza values come array vuoto
  selectedValue: any = null; // Inizializza selectedValue a null
  accountForm: FormGroup = new FormGroup({}); // Inizializza accountForm come un nuovo FormGroup


  constructor(private http: HttpClient, private accountService: AccountService) {}

  ngOnInit(): void {
    this.loadAccounts();
    console.log(this.accounts);  // Log accounts array to see its structure

  }

  loadAccounts(): void {
    this.accountService.getAccount().subscribe(
      (data: any) => {
        this.accounts = data; // Assegna i dati ricevuti alla variabile accounts
        console.log(this.accounts); // Log accounts array to see its structure
        this.errorMessage = null; // Reset error message on successful load
        this.successMessage = null; // Reset success message on successful load
      },
      (error:Error) => {
        console.error('Error loading accounts:', error);
        this.errorMessage = 'Failed to load accounts.';
      }
    );
  }

  addAccount(account: PostAccounts): void {
    // Validation (Angular's form validation or custom checks)
    for (let index = 0; index < this.accounts.$values.length; index++) {
      const accounts = this.accounts.$values[index];
       if (!accounts.nome || !accounts.voce || !accounts.valori || !accounts.dataCreazione || !accounts.valoreString) {
      alert('Please fill in all fields.');
      return;
    }
    // Ensure 'valori' is an array before passing the account
    this.accountService.createAccount(account).subscribe(
      response => {
        // Handle success response
        alert('Account created successfully');
        console.log(response);
        this.successMessage = 'Account created successfully!';
        this.errorMessage = null;},
      error => {
        // Handle error response
        alert('An error occurred while creating the account');
        console.error(error);
      }
    );
    }


    // Call the service to create the account

  }

  deleteAccount(accountId: number): void {
    this.accountService.deleteAccount(accountId).subscribe(
      (response:any) => {
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
}
