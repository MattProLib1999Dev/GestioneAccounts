import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../accountService/account.service';
import { PutAccount } from '../../models/PutAccount';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-aggiungi-account',
  standalone: true,
  imports: [],
  templateUrl: './aggiungi-account.component.html',
  styleUrl: './aggiungi-account.component.css'
})
export class AggiungiAccountComponent implements OnInit {

  accountService?: AccountService;
  accountModified: PutAccount[] = [];

  formAggiungi!: FormGroup;

  constructor(private accountService_: AccountService, private formBuilder: FormBuilder) {
    this.accountService = accountService_;
  }

  ngOnInit(): void {
    this.formAggiungi = this.formBuilder.group({
      nome: ['Seleziona', Validators.required],
      dataCreazione: ['', Validators.required]
    });
  }

  resetForm() {
    this.formAggiungi.reset({
      nome: 'Seleziona',
      dataCreazione: ''
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

  modifica(index: number, account: PutAccount) {
    this.accountService?.putAccount(index, account)
        .subscribe((result: any) => {
          this.formAggiungi.updateValueAndValidity();
          console.log(`Hai modificato l'account in posizione ${index}:`, result);
        });
}

elimina(index: number) {
    this.accountService?.deleteAccount(index)
        .subscribe((result: any) => {
          this.formAggiungi.removeControl('parentGroup');
          this.formAggiungi.updateValueAndValidity();
          console.log(`Hai eliminato l'account in posizione ${index}:`, result);
          this.accountModified.splice(index, 1);
          this.accountModified = [...this.accountModified];
        });
}


  trackByIndex(index: number, item: any): number {
    return index;
  }




}
