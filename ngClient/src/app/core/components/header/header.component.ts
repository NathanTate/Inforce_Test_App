import { Component } from '@angular/core';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  iconUser = faUser;

  constructor(public authService: AuthService) {}

  logout() {
    this.authService.logout();
  }
}
