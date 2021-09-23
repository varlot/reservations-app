import { Reservation } from "../../reservations/models/reservation";

export class Room {
    id: number;
    name: string;
    reservations: Reservation[];

    constructor() {
        this.id = 0;
        this.name = '';
        this.reservations = [];
    }
}
