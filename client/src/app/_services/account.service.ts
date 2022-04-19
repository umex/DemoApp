import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(private http: HttpClient, private router: Router) {

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
    user.roles = [];
    const roles = this.getToken(user.token).role;
    console.log(roles);
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null)
    this.router.navigateByUrl('/');
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

    getToken(token) {
      //atob converts from base64
      return JSON.parse(atob(token.split('.')[1]));
    }
}
