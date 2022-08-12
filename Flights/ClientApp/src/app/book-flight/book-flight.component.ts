import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models';


@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {

  constructor(private route: ActivatedRoute, private flightService: FlightService, private routerService: Router) { }

  flightId: string = 'not loaded'
  flight: FlightRm = {}

  ngOnInit(): void {
    this.route.paramMap
      .subscribe(parameter => this.findFlight(parameter.get("flightId")))
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findOrNullFlight({ id: this.flightId })
      .subscribe(flight => this.flight = flight,
        this.handleError)
  }

  private handleError = (err: any) => {

    if (err.status == 404)
    {
      alert("Flight not found!!")
      this.routerService.navigate(['/search-flights'])
    }

    console.log("Response Error. Status: ", err.status)
    console.log("Response Error. Status: ", err.statusText)
    console.log(err)
  }


}
