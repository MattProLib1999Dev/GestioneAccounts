// app.component.ts
import { Component } from '@angular/core';
import { AggiungiAccountComponent } from './modal/aggiungi-account/aggiungi-account.component';
import { Account } from './modal/account/account.component';
import { AggiungiValoreComponent } from './modal/aggiungi-valore/aggiungi-valore/aggiungi-valore.component';
import { AccountService } from './accountService/account.service';
import { ValoreService } from './accountService/valore/valore.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [
    Account,
    AggiungiAccountComponent,
    AggiungiValoreComponent
  ],
  providers: [AccountService, ValoreService],  // Explicitly provide service if needed
})
export class AppComponent {
  title = 'Angular Standalone App';
}
