import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  model:any = {}
  currentUser$ = this.accountService.currentUser$;

  constructor(private accountService:AccountService, private router: Router, private toastr:ToastrService) {

  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$
  }

  login(){
    this.accountService.login(this.model).subscribe(response =>{
      this.router.navigateByUrl('/books');
    },error =>{
      console.log(error)
      this.toastr.error(error.error)
    });
  }

  logout(){
    this.accountService.logout();
  }


}
