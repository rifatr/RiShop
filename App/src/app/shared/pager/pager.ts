import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  imports: [],
  templateUrl: './pager.html',
  styleUrl: './pager.scss',
})
export class Pager {
  @Input() totalCount?: number;
  @Input() pageSize?: number;
  @Input() currentPageNumber: number = 1;
  @Output() onPageChanged = new EventEmitter<number>();

  getDummyArray() {
    let totalPages =
      this.totalCount && this.pageSize 
        ? Math.ceil(this.totalCount / this.pageSize)
        : 0;

    return Array(totalPages).fill(0);
  }

  onPagerChanged(pageNumber: number) {
    if(pageNumber != this.currentPageNumber) {
      this.currentPageNumber = pageNumber;
      this.onPageChanged.emit(pageNumber);
    }
  }
}
