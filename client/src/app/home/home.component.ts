import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any
  currentUser$ = this.accountService.currentUser$;

  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event:boolean){
    this.registerMode = event;
  }

}
