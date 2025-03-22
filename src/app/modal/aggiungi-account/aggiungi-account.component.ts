import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { AccountService } from '../../accountService/account.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostAccounts } from '../../models/PostAccounts';
import { Data } from '@angular/router';

@Component({
  selector: 'app-aggiungi-account',
  templateUrl: './aggiungi-account.component.html',
  styleUrls: ['./aggiungi-account.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class AggiungiAccountComponent implements OnInit {

  formAggiungi!: FormGroup;
  accounts: any;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService) {}

  ngOnInit(): void {
    // Inizializza il form
    this.formAggiungi = this.formBuilder.group({
      nome: ['Default Nome', Validators.required],
      valoreString: ['Default String', Validators.required],
      voce: ['Default Voce', Validators.required],
      dataCreazione: [new Date().toISOString().split('T')[0], Validators.required],
      valori: this.formBuilder.array([this.createValore()])
    });

    console.log('Form inizializzato:', this.formAggiungi.value);

    this.leggiValori();
  }

  leggiValori(): void {
    this.accountService.getAccount().subscribe(
      (response: any) => {
        console.log('Account recuperati con successo', response);
        this.accounts = response;

        // Assicurati che `valori` sia un FormArray prima di modificarlo
        const valoriArray = this.formAggiungi.get('valori') as FormArray;
        valoriArray.clear(); // Pulisce eventuali valori precedenti
        response.forEach((val: any) => valoriArray.push(this.createValore(val.valore)));
      },
      (error: any) => {
        console.error('Errore nel recupero degli account', error);
      }
    );
  }

  createValore(valore: string = ''): FormGroup {
    return this.formBuilder.group({
      valore: [valore, Validators.required]
    });
  }

  addValore(): void {
    (this.formAggiungi.get('valori') as FormArray).push(this.createValore());
  }

  // Metodo per valorizzare l'oggetto PostAccounts
  creaPostAccount(): PostAccounts {
    return {
      id: 0,
      nome: this.formAggiungi.get('nome')?.value,
      valori: this.formAggiungi.get('valori')?.value,
      valoreString: this.formAggiungi.get('valoreString')?.value,
      voce: this.formAggiungi.get('voce')?.value,
      dataCreazione: this.formAggiungi.get('dataCreazione')?.value
    };
  }

  onSubmit(): void {
    if (!this.formAggiungi) {
      console.error('Il form non è stato inizializzato correttamente');
      return;
    }

    // Tocca tutti i campi per attivare la validazione
    this.formAggiungi.markAllAsTouched();

    if (this.formAggiungi.valid) {
      console.log('Form valido, invio i dati:', this.formAggiungi.value);
      const nuovoAccount = this.creaPostAccount();

      this.accountService.createAccount(nuovoAccount).subscribe(
        (response) => {
          console.log('Account creato con successo', response);
        },
        (error) => {
          console.error('Errore nella creazione dell\'account', error);
        }
      );
    } else {
      console.log('⚠️ Form non valido! Stato:', this.formAggiungi.status);
      console.log('⚠️ Errori nei campi:', this.formAggiungi.controls);
    }
  }

}
