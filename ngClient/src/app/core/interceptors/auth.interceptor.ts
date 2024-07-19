import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Observable, switchMap, take } from 'rxjs';
import { User } from '../models/User';

export class AuthInterceptor implements HttpInterceptor {
  private authService = inject(AuthService);

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return this.authService.currentUser$.pipe(take(1), 
      switchMap((user: User | null) => {
        if(!user) return next.handle(req);
        const modifiedRequest = req.clone({
          headers: req.headers.append('Authorization', 'Bearer ' + user.token)
        })
        return next.handle(modifiedRequest)
      }))
    
  }
}
