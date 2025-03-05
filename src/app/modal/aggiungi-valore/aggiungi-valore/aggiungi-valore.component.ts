import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValoreService } from '../../../accountService/valore/valore.service';
import { PutAccount } from '../../../models/PutAccount';
import { putValori } from '../../../models/valori/putValori';

@Component({
  selector: 'app-aggiungi-valore',
  standalone: true,
  imports: [],
  templateUrl: './aggiungi-valore.component.html',
  styleUrl: './aggiungi-valore.component.css'
})
export class AggiungiValoreComponent implements OnInit {

  accountService?: ValoreService;
  accountModified: PutAccount[] = [];

  formAggiungi!: FormGroup;

  constructor(private accountService_: ValoreService, private formBuilder: FormBuilder) {
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
    this.accountService_
      ?.postValori(
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

  modifica(index: number, valore: putValori) {
    this.accountService_?.putValori(index, valore)
        .subscribe((result: any) => {
          this.formAggiungi.updateValueAndValidity();
          console.log(`Hai modificato l'account in posizione ${index}:`, result);
        });
}

elimina(index: number) {
    this.accountService_?.deleteValore(index)
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
