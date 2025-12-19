export interface CartData {
  id:    string
  items: CartItem[]
}

export interface CartItem {
  id:          number
  productName: string
  price:       number
  quantity:    number
  pictureUrl:  string
  brand:       string
  type:        string
}

export class CartData implements CartData {
  id: string        = "abc";
  items: CartItem[] = [];
}

export interface CartTotal {
  shippingCharge: number;
  subtotal:       number;
  discount:       number;
  overallTotal:   number;
}
