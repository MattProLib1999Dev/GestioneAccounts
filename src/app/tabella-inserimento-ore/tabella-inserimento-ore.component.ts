import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountService } from '../accountService/account.service';
import { getAccount } from '../models/getAccount';
import { Root } from '../models/search';

@Component({
  selector: 'app-tabella-inserimento-ore',
  templateUrl: './tabella-inserimento-ore.component.html',
  styleUrls: ['./tabella-inserimento-ore.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
})
export class TabellaInserimentoOreComponent implements OnInit {
  accounts: getAccount[] = []; // Tutti gli account iniziali
  searchedAccount: Root[] | null = null; // Risultato della ricerca (null = mostra tutto)
  formAccount: FormGroup;

  giorniMese: Date[] = [];
  oreLavorate: { [accountId: string]: number[] } = {};
  totaleOreLavorate: { [accountId: string]: number } = {};

  constructor(private accountService: AccountService, private fb: FormBuilder) {
    this.formAccount = this.fb.group({
      nome: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.generaGiorniDelMeseCorrente();

    this.accountService.getAccount().subscribe({
      next: (accounts) => {
        this.accounts = accounts;

        for (const account of this.accounts) {
          this.oreLavorate[account.id] = Array(this.giorniMese.length).fill(0);
        }
      },
      error: (err) => console.error('Errore nel caricamento degli account:', err),
    });
  }

  generaGiorniDelMeseCorrente(): void {
    const oggi = new Date();
    const year = oggi.getFullYear();
    const month = oggi.getMonth();
    const daysInMonth = new Date(year, month + 1, 0).getDate();

    this.giorniMese = [];

    for (let day = 1; day <= daysInMonth; day++) {
      this.giorniMese.push(new Date(year, month, day));
    }
  }

  aggiornaOre(accountId: string, giornoIndex: number, ore: string): void {
    const oreNum = parseFloat(ore);
    if (!isNaN(oreNum)) {
      this.oreLavorate[accountId][giornoIndex] = oreNum;
      this.calcolaTotaleOrePerAccount();
    }
  }

  calcolaTotaleOrePerAccount(): void {
    this.totaleOreLavorate = {};

    for (const accountId in this.oreLavorate) {
      const ore = this.oreLavorate[accountId];
      const somma = ore.reduce((acc, curr) => acc + curr, 0);
      this.totaleOreLavorate[accountId] = somma;
    }
  }

  // Metodo per visualizzare solo i risultati filtrati se presenti
  filteredAccounts(): getAccount[] {
    const nomeRicerca = this.formAccount.get('nome')?.value?.toLowerCase().trim() || '';

    if (!nomeRicerca) {
      return this.accounts; // Se il campo è vuoto, mostra tutti
    }

    // Altrimenti mostra solo quelli che contengono il testo cercato
    return this.accounts.filter(account =>
      account.nome.toLowerCase().includes(nomeRicerca)
    );
  }


  // Ricerca account per nome
  cercaAccount(): void {
    const nome = this.formAccount.get('nome')?.value?.trim() || '';

    if (!nome) {
      this.searchedAccount = null; // Reset: mostra tutti
      return;
    }

    this.accountService.search(nome).subscribe({
      next: (results) => {
        this.searchedAccount = results;

        // Inizializza ore se non già presenti
        for (const account of results) {
          if (!this.oreLavorate[account.id]) {
            this.oreLavorate[account.id] = Array(this.giorniMese.length).fill(0);
          }
        }

        this.calcolaTotaleOrePerAccount();
      },
      error: () => {
        this.searchedAccount = []; // Nessun risultato trovato
      },
    });
  }

  deleteAccount(idUtente: string): void {
    this.accountService.deleteAccount(idUtente).subscribe({
      next: () => {
        // Rimuove da accounts principali
        this.accounts = this.accounts.filter(account => account.id !== idUtente);

        // Rimuove dai dati associati
        delete this.oreLavorate[idUtente];
        delete this.totaleOreLavorate[idUtente];

        // Rimuove anche dai risultati di ricerca
        if (this.searchedAccount) {
          this.searchedAccount = this.searchedAccount.filter(account => account.id !== idUtente);
        }
      },
      error: (err) => console.error("Errore nella cancellazione dell'account:", err)
    });
  }
}
