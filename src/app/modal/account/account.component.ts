import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../accountService/account.service';
import { PutAccount } from '../../models/PutAccount';
import { PostAccounts } from '../../models/PostAccounts';
import { AggiungiAccountComponent } from "../aggiungi-account/aggiungi-account.component";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-account',
  standalone: true,  // This ensures it's a standalone component
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
  imports: [AggiungiAccountComponent]
})
export class Account implements OnInit {

  accountService?: AccountService;
  accountadded?: PostAccounts;
  formAggiungi!: FormGroup;

  constructor(accountService_: AccountService, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.formAggiungi = this.formBuilder.group({
      nome: ['Seleziona', Validators.required],
      dataCreazione: ['', Validators.required]
    });
  }

  aggiungi() {
    this.accountService
      ?.postAccount(
        this.formAggiungi.value)
      .subscribe({
        next: (result: any) => {
          this.formAggiungi.setValue({ nome: '', dataCreazione: '' });
          console.log("Ecco il risultato", result);

        },
        error: (error: any) => {
          console.error("Errore", error)
        }
      });
  }

}
