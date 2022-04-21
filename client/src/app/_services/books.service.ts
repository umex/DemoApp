import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { catchError, map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Book } from '../_models/book';
import { BookParams } from '../_models/bookParams';
import { DeleteBookParams } from '../_models/deleteBookParams';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { AccountService } from './account.service';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token,
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,HEAD,OPTIONS,POST,PUT"
  })
}


@Injectable({
  providedIn: 'root'
})
export class BooksService {

  baseUrl: string = environment.baseUrlApi;
  books: Book[] = []
  paginatedResult: PaginatedResult<Book[]> = new PaginatedResult<Book[]>();
  bookParams: BookParams;
  user: User;
  //neke vrste dictionary key-value
  bookCache = new Map();

  constructor(private http: HttpClient , private accountService: AccountService) {
    this.bookParams = new BookParams();
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
  }

  getBooks(bookParams: BookParams, force:boolean = false) {
    if(!force){
      var response = this.bookCache.get(Object.values(bookParams).join('-'));
      const book = [...this.bookCache.values()]
      console.log('book', book)
      if (response) {
        console.log('cached response')
        return of(response);
      }
    }

    let params = new HttpParams();
    params = params.append('pageNumber', bookParams.pageNumber.toString())
    params = params.append('pageSize', bookParams.pageSize.toString())
    params = params.append('bookSearchEnum', bookParams.dropdownValue)
    if (bookParams.dropdownValue == 0) {
      params = params.append('title', bookParams.searchString)
    } else {
      params = params.append('author', bookParams.searchString)
    }


    //razisci zakaj to ne dela
    /*
    return this.http.get<Book[]>(this.baseUrl + '/book', { observe: 'response', params })
    .pipe(
      map(response => {
        console.log('not cached')
        this.paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'))
        }
        this.bookCache.set(Object.values(bookParams).join('-'), this.paginatedResult);
        return this.paginatedResult;
      })
    )
    */

    return this.getPaginatedResult<Book[]>(this.baseUrl + '/book', params).pipe(
      map(response =>{
        this.bookCache.set(Object.values(bookParams).join('-'), response);
        return response;
      })
    )


  }

  private getPaginatedResult<T>(url, params) {

    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getBook(id: Number) {
    const book = [...this.bookCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((book: Book) => book.id === id);

    if (book) {
      console.log("cached book")
      return of(book);
    }
    return this.http.get<Book>(this.baseUrl + '/book/' + id);
  }

  updateBook(book: Book) {
    //razišči zakaj ne gre PUT - zakaj prihaja do CORS errorja
    return this.http.post<Book>(this.baseUrl + '/book', book, httpOptions).pipe(
      map(() => {
        const index = this.books.indexOf(book);
        this.books[index] = book;
      }),
      catchError((err, caught) => {
        return "error";
      })
    )
  }

  lendBook(book: Book) {
    return this.http.post<Book>(this.baseUrl + '/book/lend', book, httpOptions).pipe(
      map(() => {
        const index = this.books.indexOf(book);
        this.books[index] = book;
      })
    )
  }

  deleteBook(bookId: number){
    console.log('bookid:', this.baseUrl + '/book/delete/'+ bookId)
    return this.http.post(this.baseUrl + '/book/delete/'+ bookId, httpOptions);
  }

  getBookParams() {
    return this.bookParams;
  }

  setBookParams(bookParams: BookParams) {
    this.bookParams = bookParams
  }

  resetBookParameters() {
    return this.bookParams = new BookParams()
  }
}
