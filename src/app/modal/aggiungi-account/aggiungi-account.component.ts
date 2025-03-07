import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-aggiungi-account',
  templateUrl: './aggiungi-account.component.html',
  styleUrls: ['./aggiungi-account.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule]
})
export class AggiungiAccountComponent implements OnInit {

  formAggiungi!: FormGroup;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.formAggiungi = this.formBuilder.group({
      nome: ['', Validators.required],
      valoreString: ['', Validators.required],
      voce: ['', Validators.required],
      dataCreazione: ['', Validators.required],
      valori: this.formBuilder.array([this.createValore()])
    });
  }

  createValore(): FormGroup {
    return this.formBuilder.group({
      valore: ['', Validators.required]  // Il campo 'valore' che serve nel form array
    });
  }

  addValore(): void {
    (this.formAggiungi.get('valori') as FormArray).push(this.createValore());
  }

  onSubmit() {
    if (this.formAggiungi.valid) {
      console.log('Form submitted', this.formAggiungi.value);
    } else {
      console.log('Form invalid');
    }
  }
}
