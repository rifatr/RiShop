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
  title$: Observable<string | null>;

  constructor(public bcService: BreadcrumbService) {
    this.title$ = bcService.breadcrumbs$.pipe(
      map(bc => {
        if (bc.length === 0) return null;

        const last = bc[bc.length - 1].label;
        if (last === null) return null;

        return typeof last === 'string'
          ? last
          : typeof last === 'function'
              ? last()
              : null;
      })
    );
  }
}
