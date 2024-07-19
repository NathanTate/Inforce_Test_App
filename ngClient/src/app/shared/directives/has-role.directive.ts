import { Directive, Input, OnDestroy, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { User } from '../../core/models/User';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit, OnDestroy{
  @Input('appHasRole') roles: string[] = [];
  user: User | null = null;
  subscription!: Subscription;
  hasRoleSubscription!: Subscription;

  constructor(private templateRef: TemplateRef<any>, private vcRef: ViewContainerRef,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.subscription = this.authService.currentUser$.subscribe({
      next: (user) => {
        this.user = user;
        this.hasRoleSubscription = this.authService.hasRole(this.roles).subscribe({
          next: (hasRole: boolean) => this.hasRole(hasRole)
        })
      }
    })
  }

  hasRole(hasRole: boolean) {
    if(hasRole) {
      this.vcRef.createEmbeddedView(this.templateRef);
    } else {
      this.vcRef.clear();
    }
  }

  ngOnDestroy(): void {
    if(this.subscription && this.hasRoleSubscription) {
      this.subscription.unsubscribe();
      this.hasRoleSubscription.unsubscribe();
    }
  }

}
