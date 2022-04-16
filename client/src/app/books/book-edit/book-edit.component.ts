import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Book } from 'src/app/_models/book';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { BooksService } from 'src/app/_services/books.service';

@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.css']
})
export class BookEditComponent implements OnInit {
  @ViewChild('formEdit') formEdit: NgForm
  book: Book
  user: User;
  id:Number

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.formEdit.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private bookService: BooksService, private accountService: AccountService,
    private toastr: ToastrService, private route: ActivatedRoute) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.loadBook();
  }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
  }

  loadBook() {
    //ca bi hotu ker drug parameter iskat this.route.snapshot.paramMap.get('username')
      this.bookService.getBook(Number(this.route.snapshot.paramMap.get('id'))).subscribe(book => {
        this.book = book;
      })
  }

  updateBook(){
    console.log("id:",this.id)
    this.bookService.updateBook(this.book).subscribe(() => {
      this.toastr.success('Book updated successfully');
      this.formEdit.reset(this.book);
    })
  }

}
