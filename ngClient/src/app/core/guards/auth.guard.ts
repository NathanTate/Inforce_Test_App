import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { map, take } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  let router = inject(Router);
  return inject(AuthService).currentUser$.pipe(take(1), map(user => {
    const isAuthenticated: boolean = !!user;
    if(isAuthenticated) {
      return true;
    } else {
      return router.createUrlTree(['/auth']);
    }
  })
)
}
