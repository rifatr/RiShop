import { Component } from '@angular/core';
import { BreadcrumbComponent, BreadcrumbItemDirective } from 'xng-breadcrumb';
import { ɵEmptyOutletComponent } from "@angular/router";
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-section-header',
  imports: [ BreadcrumbComponent, ɵEmptyOutletComponent, BreadcrumbItemDirective, TitleCasePipe ],
  templateUrl: './section-header.html',
  styleUrl: './section-header.scss',
})
export class SectionHeader {

}
