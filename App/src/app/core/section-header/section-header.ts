import { Component } from '@angular/core';
import { BreadcrumbComponent, BreadcrumbItemDirective, BreadcrumbService } from 'xng-breadcrumb';
import { TitleCasePipe, AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-section-header',
  imports: [ BreadcrumbComponent, BreadcrumbItemDirective, TitleCasePipe, AsyncPipe],
  templateUrl: './section-header.html',
  styleUrl: './section-header.scss',
})
export class SectionHeader {
  constructor(public bcService: BreadcrumbService) {
    const bc = bcService.breadcrumbs$;
    console.log(bc);
  }
}
