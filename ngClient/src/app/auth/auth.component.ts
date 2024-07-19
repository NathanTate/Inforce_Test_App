import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent implements OnInit{
  authForm!: FormGroup;
  loginMode = true;

  constructor(private fb: FormBuilder, private authService: AuthService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  private initializeForm() {
    this.authForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?!.*\s).{6,32}$/)]]
    })
  }

  onSubmit() {
    if (!this.authForm.valid) return;
    if(this.loginMode) {
      this.authService.login(this.authForm.value).subscribe({
        next: () => {
          this.router.navigate(['./']);
          this.authForm.reset();
        }
      })
    } else {
      this.authService.register(this.authForm.value).subscribe({
        next: () => {
          this.loginMode = !this.loginMode,
          this.password?.reset();
        }
      })
    }
  }

  get email() {
    return this.authForm.get('email');
  }

  get password() {
    return this.authForm.get('password');
  }
}
