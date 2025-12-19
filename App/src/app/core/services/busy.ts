import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  isBusy: boolean = false;

  constructor(private spinnerService: NgxSpinnerService) {}

  busy() {
    this.isBusy = true;
    this.spinnerService.show(undefined, {
      type: "timer",
      bdColor: "rgba(0, 0, 0, 0.5)",
      color: "#3333"
    });
  }

  idle() {
    this.isBusy = false;
    this.spinnerService.hide();
  }
}
