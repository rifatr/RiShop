import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  imports: [],
  templateUrl: './paging-header.html',
  styleUrl: './paging-header.scss',
})
export class PagingHeader {
  @Input() pageNumber?: number;
  @Input() pageSize?: number;
  @Input() totalCount?: number;
}
