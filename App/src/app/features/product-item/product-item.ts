import { Component, Input } from '@angular/core';
import { Product } from '../../shared/models/product';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product-item',
  imports: [ CurrencyPipe ],
  templateUrl: './product-item.html',
  styleUrl: './product-item.scss',
})
export class ProductItem {
  @Input() product?: Product;
}
