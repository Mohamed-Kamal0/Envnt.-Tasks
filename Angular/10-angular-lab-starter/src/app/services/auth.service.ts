// #Task 1: Build the AuthService (@Injectable, providedIn: 'root') from
// scratch (e.g. `ng generate service services/auth`). Inject HttpClient and
// define a SESSION_KEY constant for localStorage, then add a writable
// `isAuthenticated` signal whose initial value comes from whether a session
// marker already exists in localStorage under SESSION_KEY, so a page
// refresh doesn't sign an already-logged-in user back out to /login.

// #Task 2: Implement login(email, password): Observable<boolean>. POST the
// credentials to the backend's login endpoint (environment.apiUrl +
// '/auth/login'), mark the user as logged in on success, resolve to a
// success/failure boolean, and make a failed request resolve gracefully
// (as "not logged in") instead of throwing.

// #Task 3: Implement logout(): clear the persisted session marker from
// localStorage and set the authentication signal back to signed-out.

// #Task 4: Implement a private markLoggedIn() helper, called once a login
// attempt succeeds, that persists the session marker in localStorage and
// sets the authentication signal to signed-in.
import { HttpClient, HttpInterceptorFn } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../environments/environment";
import { Observable } from "rxjs";
import { Router } from "@angular/router";

@Injectable({ providedIn: "root" })
export class AuthService {
  constructor(
    private http: HttpClient,
    private router: Router,
  ) {}
  public isAuthenticated(): boolean {
    return localStorage.getItem("SESSION_KEY") ? true : false;
  }

  logIn(username: string, password: string) {
    return this.http
      .post<any>(`${environment.apiUrl}/Auth/login/`, {
        username,
        password,
      })
      .subscribe({
        next: (res) => {
          localStorage.setItem("SESSION_KEY", res.token);
          this.router.navigate(["/dashboard"]);
        },
        error: (err) => {
          window.alert("Please enter valid email or password.");

          console.log("error", err);
        },
      });
  }
  logOut() {
    localStorage.removeItem("SESSION_KEY");
  }
}
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem("SESSION_KEY");
  if (!token) return next(req);
  return next(
    req.clone({
      headers: req.headers.set("Authorization", `Bearer ${token}`),
    }),
  );
};
