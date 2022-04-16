import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}


@Injectable({
  providedIn: 'root'
})


export class AccountService {

  //lahko mu nastavimo vrednost v subscribeu in dobis vedno zadnjo
  private currentUserSource = new ReplaySubject<User>(1);
  //to je narejeno tako zato ker drugace bio moral biti currentUserSource public in bi ga lahko
  //kdorkoli spreminjal
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {

  }

  login(model:any){
    return this.http.post(environment.baseUrlApi + '/account/login', model).pipe(
      map((response: User) =>{
        const user = response;
        if(user){
          //napolni localstorage v browserju
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUserSource.next(user);
        }

      })
    )
  }

  setCurrentUser(user:User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null)
  }

  register(model:any){
    return this.http.post<any>(environment.baseUrlApi + '/account/register', model, httpOptions).pipe(
      map((user:User) => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUserSource.next(user);
        }
        return user;
      })

    )}
}