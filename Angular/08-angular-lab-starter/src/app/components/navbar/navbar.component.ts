import { Component } from "@angular/core";
import { RouterLink, RouterLinkActive } from "@angular/router";
/**
 * #Task 3: Import the routerLink directive and RouterLinkActive from
 * Angular's router package, add them both to this component's imports array
 * below, and update navbar.component.html to navigate through the router
 * instead of plain hrefs.
 */
@Component({
  selector: "app-navbar",
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: "./navbar.component.html",
  styleUrl: "./navbar.component.css",
})
export class NavbarComponent {}
