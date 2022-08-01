import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] = [
    {
      airline: "American Airlines",
      arrival: { time: Date.now().toString(), place: "Istanbul" },
      departure: { time: Date.now().toString(), place: "Los Angeles"},
      price: "350",
      remainingNumberOfSeats: 500
    },
    {
      airline: "Polish Airways",
      arrival: { time: Date.now().toString(), place: "Warsaw" },
      departure: { time: Date.now().toString(), place: "Berlin" },
      price: "450",
      remainingNumberOfSeats: 500
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}

export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  departure: TimePlaceRm;
  price: string;
  remainingNumberOfSeats: number;
}

export interface TimePlaceRm {
  place: string;
  time: string;
}
