import { Component, ElementRef, viewChild } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopService } from './shop.service';
import { ProductItem } from "../product-item/product-item";
import { Brand } from '../../shared/models/brand';
import { ProductType } from '../../shared/models/product-type';
import { ShopParams } from '../../shared/models/shop-params';
import { Pagination } from '../../shared/models/pagination';
import { PagingHeader } from "../../shared/paging-header/paging-header";
import { Pager } from "../../shared/pager/pager";

@Component({
  selector: 'app-shop',
  imports: [ProductItem, PagingHeader, Pager],
  templateUrl: './shop.html',
  styleUrl: './shop.scss',
})
export class Shop {
  searchTerm = viewChild<ElementRef>('search');
  products: Product[] = [];
  brands: Brand[] = []
  productTypes: ProductType[] = [];
  
  shopParams: ShopParams = new ShopParams();
  totalCount: number = 1;
  
  sortOptions = [
    {name: 'Alphabetical: A to Z', value: 'nameAsc'},
    {name: 'Alphabetical: Z to A', value: 'nameDesc'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ]
  
  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }

  updateLocalProducts(newProductResponse: Pagination<Product>) {
    this.products = newProductResponse.data;
    this.totalCount = newProductResponse.totalCount;
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.updateLocalProducts(response),
      error: err => console.error(err)
    });
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: response => this.brands = [{id: 0, name: 'All'}, ...response],
      error: err => console.error(err)
    });
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe({
      next: response => this.productTypes = [{id: 0, name: 'All'}, ...response],
      error: err => console.error(err)
    });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onProductTypeSelected(typeId: number) {
    this.shopParams.productTypeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(event: any) {
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }

  onPageNumberChanged(pageNumber: number) {
    if(pageNumber != this.shopParams.pageNumber) {
      this.shopParams.pageNumber = pageNumber;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm()?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
}
