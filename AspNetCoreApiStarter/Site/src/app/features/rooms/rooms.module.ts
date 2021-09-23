import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../../material.module'
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedModule } from '../../shared/shared.module';

// Routing
import { RoomsRoutingModule } from './rooms-routing.module';

// Components
import { RoomsComponent } from './components/rooms.component';
// import { RoomNewComponent } from './components/new/room-new.component';
// import { RoomEditComponent } from './components/edit/room-edit.component';
// import { RoomDeleteComponent } from './components/delete/room-delete.component';

// Sample with server side validation
// import { RoomNewWithServerValidationComponent } from './components/new/room-new-with-server-validation.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FlexLayoutModule,
    SharedModule,
    RoomsRoutingModule,
  ],
  declarations: [
    RoomsComponent,
    // RoomNewComponent,
    // RoomEditComponent,
    // RoomDeleteComponent,
    // RoomNewWithServerValidationComponent
  ],
})
export class RoomsModule { }
