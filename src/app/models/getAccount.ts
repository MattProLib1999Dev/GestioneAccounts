
export type getAccount = Account[]

export interface Account {
  id: string
  nome: string
  valoreString: string
  voce: string
  dataCreazione: string
  userName: string
  normalizedUserName: string
  email: string
  normalizedEmail: string
  emailConfirmed: boolean
  passwordHash: string
  securityStamp: string
  concurrencyStamp: string
  phoneNumberConfirmed: boolean
  twoFactorEnabled: boolean
  lockoutEnabled: boolean
  accessFailedCount: number
}
