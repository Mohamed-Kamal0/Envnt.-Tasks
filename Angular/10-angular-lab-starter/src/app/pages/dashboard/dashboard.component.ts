// #Task 7: Build the standalone DashboardComponent (selector 'app-dashboard',
// templateUrl './dashboard.component.html'). Scaffold with
// `ng generate component pages/dashboard --standalone` — the "hidden
// operations platform" reachable only through authGuard (Day 5 fills it with
// the real product CRUD screens). Inject AuthService and Router, and
// implement logout(): call the authentication service's logout() and
// navigate back to the login page.

import { Component, inject } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-dashboard",
  standalone: true,
  templateUrl: "./dashboard.component.html",
})
export class DashboardComponent {
  constructor(
    private AuthService: AuthService,
    private router: Router,
  ) {}
  logout() {
    console.log("loged out");
    this.AuthService.logOut();
    return this.router.navigate(["/login"]);
  }
}
