// #Task 6: Build the standalone LoginComponent (selector 'app-login',
// templateUrl './login.component.html'). Scaffold with
// `ng generate component pages/login --standalone`.
// - Import ReactiveFormsModule from '@angular/forms' (needed for the
//   [formGroup] / formControlName bindings in login.component.html) and
//   include it in the component's `imports` array.
// - Inject FormBuilder, AuthService and Router.
// - Build a FormGroup (e.g. `loginForm`) with an `email` control and a
//   `password` control, each starting empty. Put every validation rule here
//   in TypeScript — not as HTML attributes on the inputs: the email control
//   is required and must look like an email address, the password control is
//   required and needs at least six characters.
// - Also expose submitting and errorMessage state for the template.
// - Implement onSubmit(): bail out early if the form is invalid; otherwise
//   flip the submitting flag on and clear any previous error message, read
//   the email and password off the form's value, and call the authentication
//   service's login(email, password); on success, navigate to the dashboard
//   page; on failure, show a message telling the user their email or
//   password was incorrect; either way, flip the submitting flag back off
//   once the attempt finishes.

import { Component } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: "./login.component.html",
})
export class LoginComponent {
  form = new FormGroup({
    name: new FormControl("", [Validators.required, Validators.minLength(5)]),
    password: new FormControl("", [
      Validators.required,
      Validators.minLength(5),
    ]),
  });
  constructor(
    private AuthService: AuthService,
    private router: Router,
  ) {}
  submit() {
    this.AuthService.logIn(
      this.form.controls.name.value == null
        ? ""
        : this.form.controls.name.value,
      this.form.controls.password.value == null
        ? ""
        : this.form.controls.password.value,
    );
  }
}
