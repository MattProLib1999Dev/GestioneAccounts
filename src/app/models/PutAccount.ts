export interface PutAccount {
  nome: string
  valori: Valori[]
  valoreString: string
  voce: string
  dataCreazione: string
}

export interface Valori {
  valore: string
  voce: string
}
