import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { BookDto, FlightRm } from '../api/models';
import { AuthService } from '../auth/auth.service';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private flightService: FlightService,
    private routerService: Router,
    private authService: AuthService,
    private formBuilder: FormBuilder) { }

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  form = this.formBuilder.group({
    number: [1]
  })

  ngOnInit(): void {

    if (!this.authService.currentUser)
      this.routerService.navigate(['register-passenger'])

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

    if (err.status == 404) {
      alert("Flight not found!!")
      this.routerService.navigate(['/search-flights'])
    }

    console.log("Response Error. Status: ", err.status)
    console.log("Response Error. Status: ", err.statusText)
    console.log(err)
  }

  book() {
    console.log(`Booking ${this.form.get('number')?.value} passengers for the flight:${this.flight.id} `)

    const booking: BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value
    }


    this.flightService.bookFlight({ body: booking })
      .subscribe(_ => this.routerService.navigate(['/my-booking']), this.handleError)
  }
}
