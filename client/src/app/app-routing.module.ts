import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CitiesComponent } from "./cities/cities.component";
import { CityEditComponent } from "./city-edit/city-edit.component";
import { CountriesComponent } from "./countries/countries.component";
import { CountryEditComponent } from "./country-edit/country-edit.component";
import { HomeComponent } from "./home/home.component";

const routes: Routes = [
  { path: "", component: HomeComponent, pathMatch: "full" },
  { path: "cities", component: CitiesComponent },
  { path: "city/:id", component: CityEditComponent },
  { path: "city", component: CityEditComponent },
  { path: "countries", component: CountriesComponent },
  { path: "country/:id", component: CountryEditComponent },
  { path: "country", component: CountryEditComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
