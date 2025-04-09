export interface getAccount {
  $id: string
  $values: Value[]
}

export interface Value {
  $id: string
  id: number
  nome: string
  valoreString: string
  voce: string
  dataCreazione: string
}
