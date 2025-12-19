import { Component } from '@angular/core';
import { CartService } from '../../features/cart/cart.service';
import { AsyncPipe, CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-order-total',
  imports: [ AsyncPipe, CurrencyPipe ],
  templateUrl: './order-total.html',
  styleUrl: './order-total.scss',
})
export class OrderTotal {
  constructor(public cartService: CartService) {}
}
