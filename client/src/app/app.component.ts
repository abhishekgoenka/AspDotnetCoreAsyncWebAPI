import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  template: `
    <header>
      <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" href="#">WorldCities</a>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav flex-grow">
            <li
              class="nav-item"
              [routerLinkActive]="['link-active']"
              [routerLinkActiveOptions]="{ exact: true }"
            >
              <a class="nav-link text-dark" [routerLink]="['/']">Home</a>
            </li>
            <li class="nav-item" [routerLinkActive]="['link-active']">
              <a class="nav-link text-dark" [routerLink]="['/cities']"
                >Cities</a
              >
            </li>
          </ul>
        </div>
      </nav>
    </header>
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
