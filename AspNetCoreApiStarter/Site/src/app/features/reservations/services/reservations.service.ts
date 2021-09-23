import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// RxJS
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// Services
import { AppConfig } from '../../../core/services/app-config.service';

// Models
import { Reservation } from '../models/reservation';

@Injectable({
    providedIn: 'root'
})
export class ReservationsService {

    constructor(
        private http: HttpClient,
        private appConfig: AppConfig,
    ) { }

    getReservations(): Observable<Reservation[]> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/Reservations`)
            .pipe(map((resp) => resp as Reservation[]));
    }

    getReservation(id: number): Observable<Reservation> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/Reservations/${id}`)
            .pipe(map((resp) => resp as Reservation));
    }

    createReservation(reservation: Reservation): Observable<any> {
        return this.http
            .post(`${this.appConfig.config.apiUrl}/api/Reservations`, reservation);
    }

    updateReservation(reservation: Reservation): Observable<any> {
        return this.http
            .put(`${this.appConfig.config.apiUrl}/api/Reservations/${reservation.id}`, reservation);
    }

    deleteReservation(id: number): Observable<any> {
        return this.http
            .delete(`${this.appConfig.config.apiUrl}/api/Reservations/${id}`);
    }
}
