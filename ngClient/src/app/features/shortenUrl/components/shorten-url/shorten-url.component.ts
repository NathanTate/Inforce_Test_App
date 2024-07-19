import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-shorten-url',
  templateUrl: './shorten-url.component.html',
  styleUrl: './shorten-url.component.css'
})
export class ShortenUrlComponent {

  constructor(public authService: AuthService) {}
}
