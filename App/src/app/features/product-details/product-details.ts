import { Component } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopService } from '../shop/shop.service';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { BreadcrumbService } from 'xng-breadcrumb';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-product-details',
  imports: [ CurrencyPipe ],
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss',
})
export class ProductDetails {
  product?: Product;

  constructor(private shopService: ShopService, private route: ActivatedRoute, private bcService: BreadcrumbService, private cartService: CartService) {
    bcService.set('@productDetails', " "); // doesn't load the section title and breadcrumb until api call finishes
  }

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
          this.bcService.set('@productDetails', product.name);
        },
        error: error => console.log(error)
      })
    }
  }

  getProductQuantityInCart() {
    const cart = this.cartService.getCurrentCart();
    return cart?.items.find(item => item.id === this.product?.id)?.quantity ?? 0;
  }

  incrementQuantity(productId: number) {
    this.cartService.incrementExistingProductQuantity(productId);
  }

  decrementQuantity(productId: number, quantity: number = 1) {
    this.cartService.decrementQuantityOrRemoveProduct(productId, quantity);
  }

  onClickAddToCart() {
    if (this.product) {
      this.cartService.addItemInCart(this.product);
    }
  }
}
