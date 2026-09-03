import { NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  email = '';
  password = '';
  submitting = false;
  errorMessage = '';

  onSubmit(): void {
    this.submitting = true;
    this.errorMessage = '';

    this.auth.login(this.email, this.password).subscribe((success) => {
      this.submitting = false;

      if (success) {
        this.router.navigate(['/dashboard']);
      } else {
        this.errorMessage = 'Incorrect email or password.';
      }
    });
  }
}
