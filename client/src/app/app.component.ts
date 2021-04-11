import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  template: `
    <app-nav-menu></app-nav-menu>
    <main role="main">
      <div class="container">
        <router-outlet></router-outlet>
      </div>
    </main>
  `,
  styles: [],
})
export class AppComponent {
  title = "app";
}
