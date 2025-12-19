import { Injectable } from '@angular/core';
import { CartData, CartItem, CartTotal } from '../../shared/models/cart';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { BehaviorSubject } from 'rxjs';
import { Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl: string = environment.apiUrl;
  private cartSource = new BehaviorSubject<CartData | null>(null);
  cartSource$ = this.cartSource.asObservable();
  private cartTotalSource = new BehaviorSubject<CartTotal | null>(null);
  cartTotalSource$ = this.cartTotalSource.asObservable();

  constructor(private http: HttpClient) {}

  getCart(id: string) {
    return this.http.get<CartData>(this.baseUrl + 'cart?cartId=' + id).subscribe({
      next: cart => {
        this.cartSource.next(cart);
        this.calculateCartTotal();
      }
    });
  }

  setCart(cart: CartData) {
    return this.http.post<CartData>(this.baseUrl + 'cart', cart).subscribe({
      next: cart => {
        this.cartSource.next(cart);
        this.calculateCartTotal();
      }
    });
  }

  getCurrentCart() {
    return this.cartSource.value;
  }

  addItemInCart(product: Product, quantity: number = 1) {
    let item = this.mapProductToCartItem(product, quantity);
    let cart = this.getCurrentCart() ?? this.createCart();
    cart.items = this.addOrUpdateItems(cart.items, item);

    this.setCart(cart);
  }

  incrementExistingProductQuantity(productId: number) {
    const cart = this.getCurrentCart();
    if(!cart) return;

    const currentItemIndex = cart.items.findIndex(ci => ci.id === productId);
    if(currentItemIndex === -1) return;
    
    cart.items[currentItemIndex].quantity += 1;
    this.setCart(cart);
  }

  decrementQuantityOrRemoveProduct(productId: number, quantity: number = 1) {
    let cart = this.getCurrentCart();
    if (cart === null) return;

    const currentItemIndex = cart.items.findIndex(ci => ci.id === productId);
    if (currentItemIndex > -1) {
      cart.items[currentItemIndex].quantity -= quantity;
      if(cart.items[currentItemIndex].quantity < 1) {
        cart.items = cart.items.filter(item => item.id != productId);
      }

      this.setCart(cart);
    }
  }

  private addOrUpdateItems(items: CartItem[], item: CartItem): CartItem[] {
    const currentItemIndex = items.findIndex(ci => ci.id === item.id);
    if(currentItemIndex === -1) {
      items.push(item);
    } 
    else {
      items[currentItemIndex].quantity += item.quantity;
    }

    return items;
  }

  private createCart() {
    const cart = new CartData();
    localStorage.setItem("cartId", cart.id);
    return cart;
  }

  private mapProductToCartItem(product: Product, quantity: number): CartItem {
    return {
      id:          product.id,
      productName: product.name,
      price:       product.price,
      quantity:    quantity,
      pictureUrl:  product.pictureUrl,
      brand:       product.productBrand,
      type:        product.productType
    }
  }

  private calculateCartTotal() {
    const cart = this.getCurrentCart();
    if(!cart) return;

    const shippingCharge = 0;
    const discount = 0;
    const subtotal = cart.items.reduce((total, cur) => total + (cur.price * cur.quantity), 0);
    const overallTotal = subtotal + shippingCharge - discount;

    this.cartTotalSource.next({shippingCharge, subtotal, discount, overallTotal})
  }
}
