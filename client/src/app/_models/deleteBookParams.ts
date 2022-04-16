export class DeleteBookParams
{
  bookId: number
  username:string

  constructor(bookId:number, username:string){
    this.bookId = bookId
    this.username = username;
  }

}
