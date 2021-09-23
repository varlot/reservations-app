import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, AbstractControl } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { AppConfig } from '../../../../core/services/app-config.service';
import { Reservation } from '../../models/reservation';

import { cloneDeep } from 'lodash';
import { ReservationsService } from '../../services/reservations.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-reservation-new',
    templateUrl: './reservation-new-with-server-validation.component.html'
})
export class ReservationNewWithServerValidationComponent implements OnInit {
    public rsc: any;
    public form: FormGroup;

    /**
     * Creates an instance of ReservationNewWithServerValidationComponent.
     * @param {Router} router
     * @param {FormBuilder} fb
     * @param {NotifierService} notifier
     * @param {AppConfig} appConfig
     * @param {ReservationsService} reservationsService
     * @memberof ReservationNewWithServerValidationComponent
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
    }

    onCancel() {
        this.router.navigate(['/reservations']);
    }

    onSubmit() {
        const reservation = cloneDeep(this.form.value);
        reservation.roomId = Number.parseInt(reservation.roomId);
        reservation.slot = Number.parseInt(reservation.slot);
        this.reservationsService.createReservation(reservation)
            .subscribe(
                () => {
                    this.notifier.notify('success', 'Operation successfully done');
                    this.router.navigate(['reservations']);
                },
                (errResp: HttpErrorResponse) => this.handleSubmitError(errResp, this.form)
            );
    }

    handleSubmitError(resp: HttpErrorResponse, form: FormGroup): void {
        if (resp.status === 400) {
            const errors = resp.error.validation;
            const fields = Object.keys(errors || {});
            fields.forEach((field) => {
                const control = this.findFieldControl(field, form);
                control.setErrors(errors[field]);
            });
        }
    }

    findFieldControl(field: string, form: FormGroup): AbstractControl {
        let control: AbstractControl;
        if (field === 'base') {
            control = form;
        } else if (form.contains(field)) {
            control = form.get(field);
        } else if (field.match(/_id$/) && form.contains(field.substring(0, field.length - 3))) {
            control = form.get(field.substring(0, field.length - 3));
        } else if (field.indexOf('.') > 0) {
            let group = form;
            field.split('.').forEach((f) => {
                if (group.contains(f)) {
                    control = group.get(f);
                    if (control instanceof FormGroup) group = control;
                } else {
                    control = group;
                }
            })
        } else {
            // Field is not defined in form but there is a validation error for it, set it globally
            control = form;
        }
        return control;
    }
}
