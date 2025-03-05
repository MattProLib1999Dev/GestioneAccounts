export interface PostAccounts {
  id: number
  nome: string
  valori: Valori[]
  valoreString: string
  voce: string
  dataCreazione: string
}

export interface Valori {
  id: number
  voce: string
  valoreStr: string
  accountId: number
  account: string
  dataCreazione: string
  nome: string
  valoreString: string
}
