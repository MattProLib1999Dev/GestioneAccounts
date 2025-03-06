import { Component } from '@angular/core';
import { Account } from "./modal/account/account.component";
import { AggiungiAccountComponent } from "./modal/aggiungi-account/aggiungi-account.component";
import { AggiungiValoreComponent } from "./modal/aggiungi-valore/aggiungi-valore/aggiungi-valore.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [Account, AggiungiAccountComponent, AggiungiValoreComponent]
})
export class AppComponent {
  title = 'Gestione Accounts';
}
