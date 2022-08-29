/* tslint:disable */
/* eslint-disable */
import { TimePlaceRm } from './time-place-rm';
export interface BookingRm {
  airline?: null | string;
  arrival?: TimePlaceRm;
  departure?: TimePlaceRm;
  id?: string;
  numberOfBookedSeats?: number;
  passangerEmail?: null | string;
  price?: null | string;
}
