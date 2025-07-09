export interface PostAccounts {
  userName: string;
  normalizedUserName: string;
  email: string;
  normalizedEmail: string;
  emailConfirmed: boolean;
  passwordHash: string;
  securityStamp: string;
  concurrencyStamp: string;
  phoneNumber: string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd: string;
  lockoutEnabled: boolean;
  accessFailedCount: number;
  valori: Valore[];
  nome: string;
  voce: string;
  valoreString: string;
  dataCreazione: string;
}

export interface Valore {
  accountId: string;
  nome: string;
  descrizione: string;
  valoreNumerico: number;
  dataCreazione: string;
  valoreStr: string;
  voce: string;
}
