import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { CartService } from '../../features/cart/cart.service';
import { CartItem } from '../../shared/models/cart';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-top-bar',
  imports: [RouterLink, RouterLinkActive, AsyncPipe],
  templateUrl: './top-bar.html',
  styleUrl: './top-bar.scss',
})
export class TopBar {
  constructor(public cartService: CartService) {}

  getTotalItemCount(items: CartItem[]) {
    return items.reduce((prev, cur) => prev + cur.quantity, 0);
  }
}
