import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// RxJS
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// Services
import { AppConfig } from '../../../core/services/app-config.service';

// Models
import { Room } from '../models/room';

@Injectable({
    providedIn: 'root'
})
export class RoomsService {

    constructor(
        private http: HttpClient,
        private appConfig: AppConfig,
    ) { }

    getRooms(): Observable<Room[]> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/rooms`)
            .pipe(map((resp) => resp as Room[]));
    }

    getRoom(id: number): Observable<Room> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/rooms/${id}`)
            .pipe(map((resp) => resp as Room));
    }
}
