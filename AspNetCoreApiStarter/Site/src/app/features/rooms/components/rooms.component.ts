import { Component, ViewChild, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { BreakpointObserver } from "@angular/cdk/layout";

// Services
import { RoomsService } from "../services/rooms.service";
import { AppConfig } from "../../../core/services/app-config.service";

// Components
// import { RoomDeleteComponent } from "./delete/room-delete.component";

// Models
import { Room } from "../models/room";

@Component({
  selector: "app-rooms",
  templateUrl: "./rooms.component.html"
})
export class RoomsComponent implements OnInit {
  rsc: any;
  displayedColumns = ["id", "roomname", "firstname", "lastname", "actions"];
  dataSource: MatTableDataSource<Room>;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  /**
   * Creates an instance of RoomsComponent.
   * @param {Router} router
   * @param {MatDialog} dialog
   * @param {BreakpointObserver} breakpointObserver
   * @param {AppConfig} appConfig
   * @param {RoomsService} roomsService
   * @memberof RoomsComponent
   */
  constructor(
    private router: Router,
    private dialog: MatDialog,
    private breakpointObserver: BreakpointObserver,
    private appConfig: AppConfig,
    private roomsService: RoomsService
  ) {
    this.breakpointObserver
      .observe(["(max-width: 600px)"])
      .subscribe(result => {
        this.displayedColumns = ["id", "name"];
      });
  }

  ngOnInit(): void {
    this.rsc = this.appConfig.rsc.pages.rooms.table;
    this.loadRooms();
  }

  loadRooms() {
    this.roomsService.getRooms().subscribe((rooms: Room[]) => {
      this.dataSource = new MatTableDataSource<Room>(rooms);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  onEditRoom(room: Room) {
    // this.router.navigate(["rooms", "details", room.id]);
  }

  onDeleteRoom(room: Room) {
    // const dialogRef = this.dialog.open(RoomDeleteComponent, {
    //   width: "600px",
    //   data: room
    // });

    // dialogRef.afterClosed().subscribe(result => {
    //   if (result) {
    //     this.loadRooms();
    //   }
    // });
  }
}
