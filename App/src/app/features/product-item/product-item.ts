import { Component, Input } from '@angular/core';
import { Product } from '../../shared/models/product';
import { CurrencyPipe } from '@angular/common';
import { RouterLink } from "@angular/router";
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-product-item',
  imports: [ CurrencyPipe, RouterLink ],
  templateUrl: './product-item.html',
  styleUrl: './product-item.scss',
})
export class ProductItem {
  @Input() product?: Product;

  constructor(private cartService: CartService) {}

  onClickAddToCart() {
    if (this.product) {
      this.cartService.addItemInCart(this.product);
    }
  }
}
