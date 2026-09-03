import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";

import { AuthService } from "../services/auth.service";

// #Task 5: Implement the authGuard (CanActivateFn), generated with
// `ng generate guard guards/auth` (choose CanActivate when prompted). Inject
// AuthService and Router, and allow navigation to continue when the user is
// currently authenticated; otherwise redirect unauthenticated users back to
// the login page instead of letting them reach the protected route.

export const logInGuard = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isAuthenticated()) {
    return true;
  }
  //window.alert("Please log in to access the dashboard.");
  return router.createUrlTree(["/dashboard"]);
};
