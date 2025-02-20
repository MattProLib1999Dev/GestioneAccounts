import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { AccountsComponent } from './accounts/accounts.component';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClient
  ],
  exports : [
    NgModule,
    AccountsComponent
  ]
})
export class AccountModule { }
