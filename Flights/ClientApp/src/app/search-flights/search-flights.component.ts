import { Component, OnInit } from '@angular/core';
import { FlightRm } from '../api/models';
import { FlightService } from '../api/services';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] = [
   
  ]

  constructor(private flightService: FlightService) { }

  ngOnInit(): void {
  }

  search() {
    this.flightService.searchFlight({})
      .subscribe(response => this.searchResult = response, this.handleError) 
  }

  private handleError(err: any) {
    console.log("Response Error. Status: ", err.status)
    console.log("Response Error. Status: ", err.statusText)
    console.log(err)
  }

}

