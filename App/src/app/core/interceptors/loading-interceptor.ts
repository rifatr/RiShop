import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BusyService } from '../services/busy';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busy = inject(BusyService);

  busy.busy();

  return next(req).pipe(
    delay(2000),
    finalize(() => busy.idle())
  );
};
