export interface User {
  id: number;
  email: string;
  token: string | null;
  tokenExperationDate: Date;
  roles: string[];
}