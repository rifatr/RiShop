import { Component } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopService } from '../shop/shop.service';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  imports: [ CurrencyPipe ],
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss',
})
export class ProductDetails {
  product?: Product;

  constructor(private shopService: ShopService, private route: ActivatedRoute, private bcService: BreadcrumbService) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const maybeId = this.route.snapshot.paramMap.get('id');
    if (maybeId != null) {
      const id = +maybeId;
      this.shopService.getProduct(id).subscribe({
        next: product => {
          this.product = product
          this.bcService.set('@productDetails', product.name.substring(0, 15) + (product.name.length > 15 ? '...' : ''));
        },
        error: error => console.log(error)
      })
    }
  }
}
