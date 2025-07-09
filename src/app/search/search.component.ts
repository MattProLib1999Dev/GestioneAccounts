import { Component, NgModule, OnInit } from '@angular/core';
import { AccountService } from '../accountService/account.service';
import {  getAccount, getAccounts } from '../models/getAccount';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Root, search } from '../models/search';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  searchText: string = '';
  searchDate: string = '';
  searchValore: string = '';
  searchForm!: FormGroup;
  accounts!: getAccount;
  search!: search;

  filteredAccounts: getAccounts[] = [];

  nomedata: string = '';
  dataCreazioneData: string = '';
  valoreData: string = '';


  constructor(private accountService: AccountService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      nome: ['', Validators.required],
      dataCreazione: ['', Validators.required],  // corretto nome
      valoreString: ['', Validators.required]     // corretto nome
    });

  }
  onSearch(): void {
    if (this.searchForm.invalid) {
      console.log('Form non valido');
      return;
    }

    const searchData: search = this.searchForm.value;

    this.accountService.search(searchData).subscribe((result: getAccounts | null) => {
      console.log(result)
      this.filteredAccounts = result ? [result] : [];
    });
  }








}
