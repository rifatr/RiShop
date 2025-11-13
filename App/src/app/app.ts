import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { TopBar } from "./top-bar/top-bar";
import { Product } from './models/product';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TopBar, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('RiShop');
  products: Product[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>("https://localhost:7253/api/products").subscribe({
      next: response => this.products = response.data,
      error: err => console.error(err),
      complete: () => console.log('Products loaded')
    })
  }
}
