import { RouterModule, Routes } from "@angular/router";
import { AggiungiAccountComponent } from "./modal/aggiungi-account/aggiungi-account.component";
import { Account } from "./modal/account/account.component";
import { AggiungiValoreComponent } from "./modal/aggiungi-valore/aggiungi-valore/aggiungi-valore.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
  { path: 'account', component: Account },
  { path: 'aggiungi-account', component: AggiungiAccountComponent },
  { path: 'aggiungi-valore', component: AggiungiValoreComponent },
  { path: '', redirectTo: '/account', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

