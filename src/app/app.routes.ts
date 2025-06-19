import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";

export const routes: Routes = [
  {
    path: 'account',
    loadComponent: () => import('./modal/account/account.component').then(m => m.Account)
  },
  {
    path: 'aggiungi-account',
    loadComponent: () => import('./modal/aggiungi-account/aggiungi-account.component').then(m => m.AggiungiAccountComponent)
  },
  {
    path: 'aggiungi-valore',
    loadComponent: () => import('./modal/aggiungi-valore/aggiungi-valore/aggiungi-valore.component').then(m => m.AggiungiValoreComponent)
  },
];

@NgModule({
  imports: [],
  exports: [RouterModule]
})
export class AppRoutingModule {}

