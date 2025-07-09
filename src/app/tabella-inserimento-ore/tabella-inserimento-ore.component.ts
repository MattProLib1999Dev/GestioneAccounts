import { Component, OnInit } from '@angular/core';
import {  getAccount, getAccounts } from '../models/getAccount'; // Importa solo Account
import { AccountService } from '../accountService/account.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tabella-inserimento-ore',
  templateUrl: './tabella-inserimento-ore.component.html',
  standalone: true,
  imports: [CommonModule],
  styleUrls: ['./tabella-inserimento-ore.component.css'],
})
export class TabellaInserimentoOreComponent implements OnInit {
  accounts: getAccounts = []; // Cambiato da getAccount[] a getAccounts

  nomeRicerca: string = '';


  giorniMese: Date[] = [];
  oreLavorate: { [accountId: string]: number[] } = {};

  constructor(private accountService: AccountService) {}

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
    const month = oggi.getMonth(); // 0-indexed
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
    }
  }


filteredAccounts(): getAccount[] {
  if (!this.nomeRicerca.trim()) {
    return this.accounts;
  }
  const lowerSearch = this.nomeRicerca.toLowerCase();
  return this.accounts.filter(account => account.nome.toLowerCase().includes(lowerSearch));
}

onSubmitRicerca(event: Event) {
  event.preventDefault();
  // Se vuoi fare qualcosa al submit, altrimenti non serve
}
}
