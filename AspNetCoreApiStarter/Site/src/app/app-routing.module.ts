import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppLayoutComponent } from './layout/layout.component';
import { ErrorComponent } from './shared/components/error/error.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/rooms',
        pathMatch: 'full'
      },
      {
        path: 'rooms',
        loadChildren: () => import('./features/rooms/rooms.module').then(m => m.RoomsModule)
      },
      {
        path: 'reservations',
        loadChildren: () => import('./features/reservations/reservations.module').then(m => m.ReservationsModule)
      }
    ]
  },
  {
    path: 'error',
    component: ErrorComponent
  },
  {
    path: '404',
    component: NotFoundComponent
  },
  {
    path: '**',
    redirectTo: '404'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(AppRoutes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }