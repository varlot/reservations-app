import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { ReservationsComponent } from './components/reservations.component';

// Sample with server side validation
import { ReservationNewWithServerValidationComponent } from './components/new/reservation-new-with-server-validation.component';

const reservationsRoutes: Routes = [
  {
    path: '',
    component: ReservationsComponent
  },
  {
    path: 'new',
    component: ReservationNewWithServerValidationComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(reservationsRoutes)],
  exports: [RouterModule]
})
export class ReservationsRoutingModule { }
