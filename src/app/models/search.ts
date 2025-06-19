export type search = Root;

export interface Root {
  id: string
  nome: string
  valoreString: string
  voce: string
  dataCreazione: string
  userName: string
  email: string
  emailConfirmed: boolean
  passwordHash: string
  securityStamp: string
  concurrencyStamp: string
  phoneNumber: string
  phoneNumberConfirmed: boolean
  twoFactorEnabled: boolean
  lockoutEnabled: boolean
  accessFailedCount: number
}
