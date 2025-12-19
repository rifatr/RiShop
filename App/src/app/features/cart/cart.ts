import { Component } from '@angular/core';
import { CartService } from './cart.service';
import { CartItem } from '../../shared/models/cart';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { OrderTotal } from "../../shared/order-total/order-total";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-cart',
  imports: [AsyncPipe, CurrencyPipe, OrderTotal, RouterLink],
  templateUrl: './cart.html',
  styleUrl: './cart.scss',
})
export class Cart {
  constructor(public cartService: CartService) {}

  incrementQuantity(productId: number) {
    this.cartService.incrementExistingProductQuantity(productId);
  }

  decrementQuantity(productId: number, quantity: number = 1) {
    this.cartService.decrementQuantityOrRemoveProduct(productId, quantity);
  }
}
