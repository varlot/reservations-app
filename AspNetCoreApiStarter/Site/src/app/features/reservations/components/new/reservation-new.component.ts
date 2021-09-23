import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
    FormBuilder,
    FormGroup,
    Validators,
    FormControl
} from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { NotifierService } from 'angular-notifier';
import { AppConfig } from '../../../../core/services/app-config.service';
import { ReservationsService } from '../../services/reservations.service';
import { Reservation } from '../../models/reservation';

import { cloneDeep } from 'lodash';


@Component({
    selector: 'app-reservation-new',
    templateUrl: './reservation-new.component.html'
})
export class ReservationNewComponent implements OnInit {
    public rsc: any;
    public form: FormGroup;

    /**
     * Creates an instance of ReservationNewComponent.
     * @param {Router} router
     * @param {FormBuilder} fb
     * @param {NotifierService} notifier
     * @param {AppConfig} appConfig
     * @param {ReservationsService} reservationsService
     * @memberof ReservationNewComponent
     */
    constructor(
        private router: Router,
        private fb: FormBuilder,
        private notifier: NotifierService,
        private appConfig: AppConfig,
        private reservationsService: ReservationsService
    ) { }

    ngOnInit() {
        this.rsc = this.appConfig.rsc.pages.reservations.new;
        this.createForm();
    }

    createForm() {
        if (this.form) { this.form.reset(); }
        this.form = this.fb.group(new Reservation());
        this.form.controls.reservationname.setValidators(Validators.required);
        this.form.controls.email.setValidators(Validators.compose([Validators.required, CustomValidators.email]));
        this.form.controls.firstname.setValidators(Validators.required);
        this.form.controls.lastname.setValidators(Validators.required);
        this.form.controls.password.setValidators(Validators.required);
        this.form.addControl('confirmPassword', new FormControl());
        this.form.controls.confirmPassword.setValidators(CustomValidators.equalTo(this.form.controls.password));
    }

    onCancel() {
        this.router.navigate(['/reservations']);
    }

    onSubmit() {
        const reservation = cloneDeep(this.form.value);
        this.reservationsService.createReservation(reservation)
            .subscribe(
                () => {
                    this.notifier.notify('success', 'Operation successfully done');
                    this.router.navigate(['reservations']);
                });
    }
}
