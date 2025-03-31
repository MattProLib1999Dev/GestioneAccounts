export interface getAccount {
  $id: string
  $values: Value[]
}

export interface Value {
  $id: string
  id: number
  nome: string
  valori: Valori
  valoreString: string
  voce: string
  dataCreazione: string
}

export interface Valori {
  $id: string
  $values: Value2[]
}

export interface Value2 {
  $id: string
  id: number
  voce: string
  valoreStr: string
  accountId: number
  account: Account
  dataCreazione: string
  nome: string
}

export interface Account {
  $ref: string
}
