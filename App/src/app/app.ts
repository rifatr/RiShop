import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { TopBar } from "./core/top-bar/top-bar";
import { SectionHeader } from "./core/section-header/section-header";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TopBar, SectionHeader],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('RiShop');

  constructor() {}

  ngOnInit(): void {
  }
}
