import { Component } from "@angular/core";

import { NavbarComponent } from "./components/navbar/navbar.component";
import { HomeComponent } from "./pages/home/home.component";
import { FooterComponent } from "./components/footer/footer.component";

/**
 * App overview — what each piece should do, and the tasks for the pieces
 * that don't have their own `.ts`/`.html` files yet.
 *
 * #Task 1 — NavbarComponent. Path: `components/navbar/navbar.component.ts` +
 *   `.html` (create both — `navbar.component.css` in that folder is already
 *   provided, don't recreate it). Build a top navigation bar with a brand
 *   link on the left reading "ShopEase", and on the right a row of four
 *   plain (non-router) links for Home, Shop, About Us, and Contact Us.
 *   These are placeholder links for now — Day 2 turns them into real
 *   routed links.
 *
 * #Task 2 — FooterComponent. Path: `components/footer/footer.component.ts` +
 *   `.html` (create both — `footer.component.css` is already provided).
 *   Build a footer with two short lines of muted text side by side — on
 *   the left, credit text reading "ShopEase · built in this Angular
 *   course", and on the right, a copyright line showing the current year
 *   (you'll likely want a `year` property on the class for this).
 *
 * #Task 3 — HeroComponent. Path: `components/hero/hero.component.ts` +
 *   `.html` (create both — `hero.component.css` is already provided). Build
 *   a hero banner section with a small badge label reading "New season", a
 *   large headline reading "Everything you need, one cart away.", a short
 *   descriptive paragraph, and a call-to-action link reading "Shop now".
 *
 * - ProductsComponent — stays an empty placeholder for now; it starts
 *   rendering real product data in Day 2. Not one of today's tasks.
 *
 * Pages (assembled from the components above, see #Task 4-7 in their own
 * files):
 * - HomeComponent      — composes Navbar + Hero + Products + Footer, in that
 *   order. This is the only page actually shown today (see below).
 * - ShopComponent      — a "Shop" heading plus the Products component.
 * - AboutUsComponent   — an "About Us" heading plus an introductory paragraph.
 * - ContactUsComponent — a "Contact Us" heading plus a simple form (email +
 *   message + Send button) that isn't wired up to anything yet.
 *
 * DAY 1 — no router yet, so the root component just shows the home page.
 */

@Component({
  selector: "app-root",
  standalone: true,
  imports: [NavbarComponent, HomeComponent, FooterComponent],
  templateUrl: "./app.component.html",
})
export class AppComponent {}
