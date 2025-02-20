export interface PostAccounts {
  id: number
  nome: string
  valori: Valori[]
}

export interface Valori {
  id: number
  voce: string
  valore: string
  accountId: number
}
