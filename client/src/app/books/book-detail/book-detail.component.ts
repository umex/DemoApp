import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { moment } from 'ngx-bootstrap/chronos/testing/chain';
import { BsDatepickerNavigationViewComponent } from 'ngx-bootstrap/datepicker/themes/bs/bs-datepicker-navigation-view.component';
import { ToastrService } from 'ngx-toastr';
import { Book } from 'src/app/_models/book';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { BooksService } from 'src/app/_services/books.service';


@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit {

  book: Book;
  user:User;
  lendForm: FormGroup;
  registerForm: FormGroup;
  minDate: Date;
  validationErrors: string[] = [];


  constructor(private bookService: BooksService, private router: ActivatedRoute, private route: Router,
                      private formBuilder: FormBuilder, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.minDate = new Date();
    this.intitializeForm();
    this.loadBook();

  }

  intitializeForm() {
    this.lendForm = this.formBuilder.group({
      lendFrom: ['', [Validators.required, this.lessThanTwo()]]
      //created: ['', Validators.required],
    })
  }

  loadBook() {
    //ce bi hotu ker drug parameter iskat this.route.snapshot.paramMap.get('username')
    this.bookService.getBook(Number(this.router.snapshot.paramMap.get('id'))).subscribe(book => {
      this.book = book;
    })
  }

  return(){
    this.route.navigateByUrl('/', {skipLocationChange: true}).then(()=>
   this.route.navigate(['books']));
  }

  lendBook() {
    this.book.lentOut = true;
    this.book.lendFrom = this.lendForm.get('lendFrom').value
    var dateTo = new Date();
    dateTo.setDate(this.book.lendFrom.getDate() +14)
    this.book.lendTo = dateTo

    this.bookService.lendBook(this.book).subscribe(() => {
      this.toastr.success('Book lend out successfully');
      this.route.navigateByUrl('/book')
      this.lendForm.reset(this.book);
    })
  }

  lessThanTwo(): ValidatorFn {
    return (control: AbstractControl) => {
      const dateValue = new Date(control.value);
      var dateNow = new Date();
      dateNow.setDate(dateNow.getDate() +14)

      if(dateValue <= dateNow){
        console.log("returning null")
        return null
      }
      return {invalidDate: true};
    }
  }




}
