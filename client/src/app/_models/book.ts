import { AppUser } from "./appUser";

export interface Book {
  id: number;
  created: string;
  title: string;
  description: string;
  author: string;
  lentOut:boolean;
  user:AppUser;
  lendFrom:Date;
  lendTo:Date;
}
