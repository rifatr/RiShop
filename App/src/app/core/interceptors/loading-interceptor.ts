import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Busy } from '../services/busy';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busy = inject(Busy);

  busy.busy();

  return next(req).pipe(
    delay(2000),
    finalize(() => busy.idle())
  );
};
