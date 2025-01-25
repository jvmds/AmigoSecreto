import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {ProductMenu} from '../models/menu/product-menu';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  protected readonly PRODUCTS: ProductMenu[] = [
    {
      name: "grupos",
      description: "grupos",
      byProducts: [
        {
          name: "buscar grupo",
          description: "buscar grupo",
          byProducts: []
        },
        {
          name: "criar grupo",
          description: "criar grupo",
          byProducts: []
        }
      ]
    }
  ]

  constructor() { }

  getProducts(): Observable<ProductMenu[]> {
    return new Observable<ProductMenu[]>((subscriber) => {
      subscriber.next(this.PRODUCTS);
      subscriber.complete();
    })
  }
}
