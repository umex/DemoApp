import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appUser';

//tegaq ne rabimo vec z interceptorjem, potestiraj
//endpointi morja bit poauthenticiran
const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl:string = environment.baseUrlApi;

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<AppUser[]>(this.baseUrl + '/users', httpOptions);
  }

  getUser(username: string) {
    return this.http.get<AppUser>(this.baseUrl + '/users/' + username, httpOptions);
  }
}
