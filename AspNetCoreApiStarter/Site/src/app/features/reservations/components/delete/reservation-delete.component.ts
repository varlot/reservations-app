import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { NotifierService } from 'angular-notifier';
import { AppConfig } from '../../../../core/services/app-config.service';
import { ReservationsService } from '../../services/reservations.service';
import { Reservation } from '../../models/reservation';

@Component({
  selector: 'app-reservation-delete',
  templateUrl: './reservation-delete.component.html'
})
export class ReservationDeleteComponent implements OnInit {
  rsc: any;

  /**
   * Creates an instance of ReservationDeleteComponent.
   * @param {NotifierService} notifier
   * @param {AppConfig} appConfig
   * @param {ReservationsService} reservationsService
   * @param {MatDialogRef<ReservationDeleteComponent>} dialogRef
   * @param {Reservation} reservation
   * @memberof ReservationDeleteComponent
   */
  constructor(
    private notifier: NotifierService,
    private appConfig: AppConfig,
    private reservationsService: ReservationsService,
    public dialogRef: MatDialogRef<ReservationDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public reservation: Reservation
  ) { }

  ngOnInit() { }

  onCancel() {
    this.dialogRef.close(false);
  }

  onDelete() {
    this.reservationsService.deleteReservation(this.reservation.id)
      .subscribe(
        (resp) => {
          this.dialogRef.close(true);
          this.notifier.notify('success', 'Operation successfully done !');
        }
      );
  }
}
