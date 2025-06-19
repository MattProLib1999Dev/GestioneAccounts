import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AccountService } from '../../accountService/account.service';
import { PostAccounts } from '../../models/PostAccounts';
import { AggiungiAccountComponent } from "../aggiungi-account/aggiungi-account.component";
import { AggiungiValoreComponent } from '../aggiungi-valore/aggiungi-valore/aggiungi-valore.component';
import { SearchComponent } from "../../search/search.component";

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AggiungiAccountComponent,
    AggiungiValoreComponent,
    SearchComponent
  ],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
})
export class Account implements OnInit {

  formAggiungi!: FormGroup;
  accountadded?: PostAccounts;

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.formAggiungi = this.formBuilder.group({
      nome: ['Seleziona', Validators.required],
      dataCreazione: ['', Validators.required]
    });
  }

  onAnimationEnd(): void {
    this.router.navigate(['/aggiungi-account']);
  }
}
