import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/internal/Observable';
import { Book } from 'src/app/_models/book';
import { BookParams } from 'src/app/_models/bookParams';
import { Pagination } from 'src/app/_models/pagination';
import { BooksService } from 'src/app/_services/books.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {

  books: Book[];
  pagination: Pagination;
  bookParams: BookParams;
  bookDropdownOptions = [{value: 0, display:"Title"}, {value: 1, display:"Author"}]

  constructor(private bookService: BooksService, private toastr: ToastrService) {
    this.bookParams = bookService.getBookParams();
  }

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(force:boolean = false){
    this.bookParams = this.bookService.getBookParams();
    this.bookService.getBooks(this.bookParams,force).subscribe(response => {
      this.books = response.result;
      this.pagination = response.pagination;
    });
  }

  resetFilters(){
    this.bookParams = this.bookService.resetBookParameters();
    this.loadBooks();
  }

  pageChanged(event:any){
      this.bookParams.pageNumber = event.page
      this.loadBooks();
  }

  deleteBook(bookId:number){
    this.bookService.deleteBook(bookId).subscribe(data =>{
      this.toastr.success('Book deleted successfully');
      this.loadBooks(true);

    });
  }


}
