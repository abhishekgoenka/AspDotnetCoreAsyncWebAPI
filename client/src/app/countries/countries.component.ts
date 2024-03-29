import { ViewChild } from "@angular/core";
import { Component, OnInit } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatSort, SortDirection } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Subject } from "rxjs";
import { debounceTime, distinctUntilChanged } from "rxjs/operators";
import { ApiResult } from "../base.service";
import { Country } from "./country";
import { CountryService } from "./country.service";

@Component({
  selector: "app-countries",
  templateUrl: "./countries.component.html",
  styleUrls: ["./countries.component.css"],
})
export class CountriesComponent implements OnInit {
  public displayedColumns: string[] = [
    "id",
    "name",
    "iso2",
    "iso3",
    "totCities",
  ];
  public countries!: MatTableDataSource<Country>;
  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = "name";
  public defaultSortOrder: SortDirection = "asc";
  defaultFilterColumn: string = "name";
  filterQuery: string = "";
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  filterTextChanged: Subject<string> = new Subject<string>();

  constructor(private countryService: CountryService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData2(event: Event) {
    const { target } = event;
    if (target) {
      this.onFilterTextChanged((<any>target).value);
    }
  }

  loadData(query: string = "") {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    if (query) {
      this.filterQuery = query;
    }
    this.getData(pageEvent);
  }

  getData(event: PageEvent) {
    var sortColumn = this.sort ? this.sort.active : this.defaultSortColumn;
    var sortOrder = this.sort ? this.sort.direction : this.defaultSortOrder;
    var filterColumn = this.filterQuery ? this.defaultFilterColumn : null;
    var filterQuery = this.filterQuery ? this.filterQuery : null;

    this.countryService
      .getData<ApiResult<Country>>(
        event.pageIndex,
        event.pageSize,
        sortColumn,
        sortOrder,
        filterColumn,
        filterQuery
      )
      .subscribe(
        (result) => {
          this.paginator.length = result.totalCount;
          this.paginator.pageIndex = result.pageIndex;
          this.paginator.pageSize = result.pageSize;
          this.countries = new MatTableDataSource<Country>(result.data);
        },
        (error) => console.error(error)
      );
  }

  // debounce filter text changes
  onFilterTextChanged(filterText: string) {
    if (this.filterTextChanged.observers.length === 0) {
      this.filterTextChanged
        .pipe(debounceTime(1000), distinctUntilChanged())
        .subscribe((query) => {
          this.loadData(query);
        });
    }
    this.filterTextChanged.next(filterText);
  }
}
