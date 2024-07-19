import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map, take } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { User } from "../models/User";
import { environment } from "../../../environments/environment.development";
import { AuthModel } from "../models/AuthModel";


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  currentUserSubject = new BehaviorSubject<User| null>(null);
  currentUser$ = this.currentUserSubject.asObservable();
  tokenExperationTimer!: ReturnType<typeof setTimeout>;
  baseUrl = environment.apiUrl;
  private _userId!: number;

  constructor(private http: HttpClient, private router: Router) {
   
  }

  get userId() {
    return this._userId
  }

  login(model: AuthModel) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((user: User) => {
          this.setCurrentUser(user);
      })
    );
  }
  
  hasRole(roles: string[]): Observable<boolean> {
    return this.currentUser$.pipe(take(1), map(user => {
      if(!user || !user.roles) return false;
      return user.roles.some(role => roles.includes(role.toUpperCase()));
    }))
  }

  register(model: AuthModel) { 
    return this.http.post(this.baseUrl + 'account/register', model, {responseType: 'text'});
  }

  autoLogin() {
    let user: User = JSON.parse(localStorage.getItem('user') ?? '{}')
    if(Object.keys(user).length === 0) return;
    this.setCurrentUser(user);
  }

  logout() {
    this.currentUserSubject.next(null);
    this.router.navigate(['/auth'])
    if(this.tokenExperationTimer) {
      clearTimeout(this.tokenExperationTimer)
    }
    localStorage.removeItem('user');
  }

  autoLogout(expiresIn: number) {
    this.tokenExperationTimer = setTimeout(() => this.logout(), expiresIn);
  }

  setCurrentUser(user: User) {
    const decodedToken = this.getDecodedToken(user.token);
    const expires = new Date(decodedToken.exp * 1000)
    const experationTime = expires.getTime() - Date.now();
    const roles = decodedToken.role;
    user.tokenExperationDate = expires;
    user.roles = [];
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    const isValidToken = user.tokenExperationDate && new Date() < user.tokenExperationDate;
    if(isValidToken) {
      this._userId = user.id;
      this.currentUserSubject.next(user);
      localStorage.setItem('user', JSON.stringify(user));
      this.autoLogout(experationTime);
    }
  }

  getDecodedToken(token: string | null) {
    if(token) {
      return JSON.parse(atob(token.split('.')[1]))
    }
  }

}