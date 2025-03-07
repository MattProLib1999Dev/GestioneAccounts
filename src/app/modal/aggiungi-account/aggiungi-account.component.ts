import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { AccountService } from '../../accountService/account.service'; // Importa il servizio
import { CommonModule } from '@angular/common'; // Importa il modulo comune
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Moduli necessari per i form

@Component({
  selector: 'app-aggiungi-account',
  templateUrl: './aggiungi-account.component.html',
  styleUrls: ['./aggiungi-account.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class AggiungiAccountComponent implements OnInit {

  formAggiungi!: FormGroup;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService) { }

  ngOnInit(): void {
    // Inizializza il form con i suoi controlli
    this.formAggiungi = this.formBuilder.group({
      nome: ['', Validators.required],  // Controllo obbligatorio per 'nome'
      valoreString: ['', Validators.required],  // Controllo obbligatorio per 'valoreString'
      voce: ['', Validators.required],  // Controllo obbligatorio per 'voce'
      dataCreazione: ['', Validators.required],  // Controllo obbligatorio per 'dataCreazione'
      valori: this.formBuilder.array([this.createValore()])  // Aggiungi un primo valore nel FormArray
    });
  }

  // Crea un nuovo FormGroup per ogni valore nel FormArray
  createValore(): FormGroup {
    return this.formBuilder.group({
      valore: ['', Validators.required]  // Ogni valore deve essere obbligatorio
    });
  }

  // Funzione per aggiungere un nuovo valore al FormArray
  addValore(): void {
    (this.formAggiungi.get('valori') as FormArray).push(this.createValore());
  }

  // Funzione per inviare i dati al server tramite il servizio createAccount
  onSubmit(): void {
    if (this.formAggiungi.valid) {
      console.log('Form submitted', this.formAggiungi.value);  // Log del form

      const nome = this.formAggiungi.get('nome')?.value;
      const valoreString = this.formAggiungi.get('valoreString')?.value;
      const voce = this.formAggiungi.get('voce')?.value;

      // Recupera i valori dal FormArray
      const valori = (this.formAggiungi.get('valori') as FormArray).controls.map((control: any) => {
        return { valore: control.get('valore')?.value };
      });

      // Invio dei dati al server tramite il servizio
      this.accountService.createAccount(nome, valoreString, voce, valori).subscribe(
        (response) => {
          console.log('Account creato con successo', response);  // Successo nella creazione
        },
        (error) => {
          console.error('Errore nella creazione dell\'account', error);  // Gestione dell'errore
        }
      );
    } else {
      console.log('Form non valido');  // Se il form non è valido
    }
  }
}
