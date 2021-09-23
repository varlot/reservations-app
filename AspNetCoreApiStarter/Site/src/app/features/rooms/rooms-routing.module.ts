import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { RoomsComponent } from './components/rooms.component';
// import { RoomNewComponent } from './components/new/room-new.component';
// import { RoomEditComponent } from './components/edit/room-edit.component';

// Sample with server side validation
// import { RoomNewWithServerValidationComponent } from './components/new/room-new-with-server-validation.component';

const roomsRoutes: Routes = [
  {
    path: '',
    component: RoomsComponent
  },
  // {
  //   path: 'new',
  //   component: RoomNewWithServerValidationComponent
  // },
  // {
  //   path: 'edit/:id',
  //   component: RoomEditComponent
  // }
];

@NgModule({
  imports: [RouterModule.forChild(roomsRoutes)],
  exports: [RouterModule]
})
export class RoomsRoutingModule { }
