import { Component } from '@angular/core';
import { BreadcrumbComponent, BreadcrumbItemDirective, BreadcrumbService } from 'xng-breadcrumb';
import { TitleCasePipe, AsyncPipe } from '@angular/common';
import { map, Observable } from 'rxjs';

@Component({
  selector: 'app-section-header',
  imports: [ BreadcrumbComponent, BreadcrumbItemDirective, TitleCasePipe, AsyncPipe],
  templateUrl: './section-header.html',
  styleUrl: './section-header.scss',
})
export class SectionHeader {
  titleBreadcrumb$: Observable<any>;

  constructor(public bcService: BreadcrumbService) {
    this.titleBreadcrumb$ = bcService.breadcrumbs$.pipe(
      map(bc => {
        if (bc.length === 0) return null;

        return bc[bc.length - 1];
      })
    );
  }
}
