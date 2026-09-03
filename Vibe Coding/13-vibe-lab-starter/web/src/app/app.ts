import { Component } from "@angular/core";
import { ProductList } from "./product-list";

// The root shell. One page, on purpose — the router is Week 2's lesson, not this
// week's. Week 3 is about how you and the AI change code like this together.
@Component({
  selector: "app-root",
  standalone: true,
  imports: [ProductList],
  template: `
    <div class="app">
      <header class="topbar">
        <h1><i class="fa-solid fa-store"></i> Product Catalog</h1>
        <span class="text-muted">.NET API + Angular · Vibe Coding bench</span>
      </header>

      <main>
        <app-product-list />
      </main>
    </div>
  `,
})
export class App {}
