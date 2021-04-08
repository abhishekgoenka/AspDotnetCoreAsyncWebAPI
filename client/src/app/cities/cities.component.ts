import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { City } from "./city";

@Component({
  selector: "app-cities",
  templateUrl: "./cities.component.html",
  styleUrls: ["./cities.component.css"],
})
export class CitiesComponent implements OnInit {
  public cities: City[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<City[]>("api/Cities").subscribe(
      (result) => {
        this.cities = result;
      },
      (error) => console.error(error)
    );
  }
}
