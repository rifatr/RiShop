import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { TopBar } from "./core/top-bar/top-bar";
import { Shop } from './features/shop/shop';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TopBar, Shop],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('RiShop');

  constructor() {}

  ngOnInit(): void {
  }
}
