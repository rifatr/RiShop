import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgxSpinnerComponent } from 'ngx-spinner'

import { TopBar } from "./core/top-bar/top-bar";
import { SectionHeader } from "./core/section-header/section-header";
import { CartService } from './features/cart/cart.service';

@Component({
  selector: 'app-root',
  imports: [ RouterOutlet, TopBar, SectionHeader, NgxSpinnerComponent ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('RiShop');

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    const cartId = localStorage.getItem("cartId");
    if (cartId) this.cartService.getCart(cartId);
  }
}
