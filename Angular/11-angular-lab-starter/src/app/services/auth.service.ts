import { HttpClient } from "@angular/common/http";
import { Injectable, inject, signal } from "@angular/core";
import { Observable, of } from "rxjs";
import { catchError, delay, map, tap } from "rxjs/operators";

import { environment } from "../environments/environment";

const SESSION_KEY = "shopease_logged_in";

@Injectable({ providedIn: "root" })
export class AuthService {
  private readonly http = inject(HttpClient);

  readonly isAuthenticated = signal<boolean>(
    localStorage.getItem(SESSION_KEY) === "true",
  );

  login(email: string, password: string): Observable<boolean> {
    // if (environment.useMockAuth) {
    //   const ok = email.trim().length > 0 && password.trim().length >= 6;
    //   return of(ok).pipe(
    //     delay(400),
    //     tap((success) => {
    //       if (success) this.markLoggedIn();
    //     })
    //   );
    // }

    return this.http
      .post<{
        token: string;
      }>(`${environment.apiUrl}/auth/login`, { email, password })
      .pipe(
        tap(() => this.markLoggedIn()),
        map(() => true),
        catchError(() => of(false)),
      );
  }

  logout(): void {
    localStorage.removeItem(SESSION_KEY);
    this.isAuthenticated.set(false);
  }

  private markLoggedIn(): void {
    localStorage.setItem(SESSION_KEY, "true");
    this.isAuthenticated.set(true);
  }
}
