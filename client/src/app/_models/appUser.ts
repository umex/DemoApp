import { Book } from "./book";
import { LibraryLedger } from "./libraryLedger";

export interface AppUser {
    id: number;
    userName: string;
    created: string;
    ledgers: Book[];
}
