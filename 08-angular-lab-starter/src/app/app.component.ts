import { Component } from "@angular/core";

import { FooterComponent } from "./components/footer/footer.component";
import { NavbarComponent } from "./components/navbar/navbar.component";
import { RouterOutlet } from "@angular/router";

/**
 * DAY 2 — the navbar and footer move here, once, and every routed page renders
 * inside <router-outlet>. Individual pages no longer include their own navbar.
 */
@Component({
  selector: "app-root",
  standalone: true,
  imports: [NavbarComponent, FooterComponent, RouterOutlet],
  template: `
    <app-navbar />
    <!-- #Task 5: Import RouterOutlet, add it to the imports array above, and
         place the router outlet here between the navbar and footer so
         routed pages render inside the shell. -->
    <router-outlet/>
    <app-footer />
  `,
})
export class AppComponent {}
