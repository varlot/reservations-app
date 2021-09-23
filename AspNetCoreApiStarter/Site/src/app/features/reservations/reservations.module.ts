import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../../material.module'
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedModule } from '../../shared/shared.module';

// Routing
import { ReservationsRoutingModule } from './reservations-routing.module';

// Components
import { ReservationsComponent } from './components/reservations.component';
import { ReservationNewComponent } from './components/new/reservation-new.component';
import { ReservationDeleteComponent } from './components/delete/reservation-delete.component';

// Sample with server side validation
import { ReservationNewWithServerValidationComponent } from './components/new/reservation-new-with-server-validation.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FlexLayoutModule,
    SharedModule,
    ReservationsRoutingModule,
  ],
  declarations: [
    ReservationsComponent,
    ReservationNewComponent,
    ReservationDeleteComponent,
    ReservationNewWithServerValidationComponent
  ],
})
export class ReservationsModule { }
