import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../accountService/account.service';
import { PutAccount } from '../../models/PutAccount';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PostAccounts } from '../../models/PostAccounts';

@Component({
  selector: 'app-aggiungi-account',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './aggiungi-account.component.html',
  styleUrl: './aggiungi-account.component.css'
})
export class AggiungiAccountComponent implements OnInit {

  accountService?: AccountService;
  account: PutAccount[] = [];
  postAccount?: PostAccounts;

  formAggiungi!: FormGroup;
  nome: string = "";
  valore: string = "";
  valoreString: string = "";
  dataCreazione: string = "";
  errorMessage: string = "errore";
  errors: any;

  constructor(private accountService_: AccountService, private formBuilder: FormBuilder) {
    this.accountService = accountService_;
  }

  ngOnInit(): void {
    this.formAggiungi = new FormGroup({
      valore: new FormControl(null, [Validators.required, Validators.min(1)]), // Aggiungi validazione per l'ID
      nome: new FormControl('', Validators.required),
      valoreString: new FormControl('', [Validators.required]),
      dataCreazione: new FormControl('', [Validators.required])
    });

    this.account = [{
      nome: "",
      valori: [
        { valore: "", voce: "" },
        { valore: "", voce: "" }
      ],
      valoreString: "",
      voce: "",
      dataCreazione: ""
    }];


    console.log(this.account);
  }


  resetForm() {
    this.formAggiungi.reset({
      nome: 'Seleziona',
      dataCreazione: ''
    });
  }

  aggiungi() {
    console.log('Chiamata al metodo aggiungi');

    if (this.formAggiungi.valid) {
      console.log('Form valido', this.formAggiungi.value);

      const formValues = this.formAggiungi.value;
      console.log('Valori del form:', formValues);

      // Costruisci l'oggetto postAccount dai valori del form
      this.postAccount = {
        id: 0,
        nome: formValues.nome,
        valori: formValues.valore,
        valoreString: formValues.valoreString,
        voce: "", // Add the missing property 'voce'
        dataCreazione: formValues.dataCreazione
      };

      console.log('postAccount:', this.postAccount);

      // Effettua la chiamata al servizio
      this.accountService?.postAccount(this.formAggiungi.value).subscribe(
        (response) => {
          console.log('Account creato con successo', response);
        },
        (error) => {
          console.log('Errore nella chiamata HTTP:', error);
          if (error.status === 400) {
            console.log('Errore 400: Bad Request');
            console.log('Dettagli dell\'errore:', error.error.errors); // Dettagli di validazione
            this.errors = error.error.errors; // Memorizza gli errori per mostrarli nell'interfaccia utente
          } else {
            console.log('Errore sconosciuto', error);
          }
        }
      );
    } else {
      console.log('Il form non è valido');
    }
  }



  updateAccount(accountId: number): void {
    if (this.formAggiungi.valid) {
      const formValues = this.formAggiungi.value;
      console.log('Form Values:', formValues);

      // Rimuovi l'ID dal corpo della richiesta, se è nullo o zero
      if (formValues.id === null || formValues.id === 0) {
        delete formValues.id;
      }

      this.accountService?.putAccount(accountId, formValues).subscribe(
        response => {
          console.log('Account updated successfully');
        },
        error => {
          console.error('Error updating account', error);
        }
      );
    }
  }

  elimina(index: number) {
      this.accountService?.deleteAccount(index)
          .subscribe((result: any) => {
            this.formAggiungi.removeControl('parentGroup');
            this.formAggiungi.updateValueAndValidity();
            console.log(`Hai eliminato l'account in posizione ${index}:`, result);
          });
  }


  trackByIndex(index: number, item: any): number {
    return index;
  }




}
