import { AppUser } from "./appUser";
import { Book } from "./book";

export interface LibraryLedger {
    id: number;
    user: AppUser;
    book: Book;
    created: string;
    lentFrom: string;
    lentTo: string;
    lentOut: boolean;
    overdue: boolean;
}
