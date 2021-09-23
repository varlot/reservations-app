import { Component, ViewChild, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { BreakpointObserver } from "@angular/cdk/layout";

// Services
import { ReservationsService } from "../services/reservations.service";
import { AppConfig } from "../../../core/services/app-config.service";

// Components
import { ReservationDeleteComponent } from "./delete/reservation-delete.component";

// Models
import { Reservation } from "../models/reservation";

@Component({
  selector: "app-reservations",
  templateUrl: "./reservations.component.html"
})
export class ReservationsComponent implements OnInit {
  rsc: any;
  displayedColumns = ["id", "reservationname", "firstname", "lastname", "actions"];
  dataSource: MatTableDataSource<Reservation>;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  /**
   * Creates an instance of ReservationsComponent.
   * @param {Router} router
   * @param {MatDialog} dialog
   * @param {BreakpointObserver} breakpointObserver
   * @param {AppConfig} appConfig
   * @param {ReservationsService} reservationsService
   * @memberof ReservationsComponent
   */
  constructor(
    private router: Router,
    private dialog: MatDialog,
    private breakpointObserver: BreakpointObserver,
    private appConfig: AppConfig,
    private reservationsService: ReservationsService
  ) {
    this.breakpointObserver
      .observe(["(max-width: 600px)"])
      .subscribe(result => {
        this.displayedColumns = ["username", "roomId", "date", "slot", "actions"];
      });
  }

  ngOnInit(): void {
    this.rsc = this.appConfig.rsc.pages.reservations.table;
    this.loadReservations();
  }

  loadReservations() {
    this.reservationsService.getReservations().subscribe((reservations: Reservation[]) => {
      this.dataSource = new MatTableDataSource<Reservation>(reservations);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  onAddReservation() {
    this.router.navigate(["reservations", "new"]);
  }

  onEditReservation(reservation: Reservation) {
    this.router.navigate(["reservations", "edit", reservation.id]);
  }

  onDeleteReservation(reservation: Reservation) {
    const dialogRef = this.dialog.open(ReservationDeleteComponent, {
      width: "600px",
      data: reservation
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadReservations();
      }
    });
  }

  private toto(test: boolean, obj: any): boolean {
    // TODO
    return true;
  }
}
