import { Component } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopService } from '../shop/service/shop';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product-details',
  imports: [ CurrencyPipe ],
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss',
})
export class ProductDetails {
  product?: Product;

  constructor(private shopService: ShopService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const maybeId = this.route.snapshot.paramMap.get('id');
    if (maybeId != null) {
      const id = +maybeId;
      this.shopService.getProduct(id).subscribe({
        next: product => this.product = product,
        error: error => console.log(error)
      })
    }
  }
}
