import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  //ce posiljamo podatke parentu
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  maxDate: Date;
  validationErrors: string[] = [];

  //ce dobivamo podatke od parenta
  @Input() usersFromHomeComponent:any;

  constructor(private accountService: AccountService, private toastr:ToastrService,
    private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.intitializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
  }

  register() {
    console.log('model: ', this.registerForm.value)
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/book')
    }, error => {
      this.validationErrors = error;
      //this.validationErrors = error.error;
      console.log('error',this.validationErrors);
    })
  }

  intitializeForm() {
    /*
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]),
      confirmPassword: new FormControl('',  [Validators.required, this.matchValues('password')]),
    })
    this.registerForm.controls.password.valueChanges.subscribe(()=>{
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
    */

    this.registerForm = this.formBuilder.group({
      username: ['', [Validators.required,
        Validators.minLength(2), Validators.maxLength(10)]],
      password: ['', [Validators.required,
        Validators.minLength(4), Validators.maxLength(20)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
      //created: ['', Validators.required],
    })

  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value ? null : {isMatching: true}
    }
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
