export class Reservation {
    id: number;
    username: string;
    roomId: number;
    date: string;
    slot: number;

    constructor() {
        this.id = 0;
        this.username = '';
        this.roomId = 0;
        this.date = '';
        this.slot = 0;
    }
}
