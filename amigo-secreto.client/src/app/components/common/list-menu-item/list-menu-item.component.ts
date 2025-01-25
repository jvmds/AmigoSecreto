import {Component, Input, OnInit} from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatIconButton} from '@angular/material/button';
import {Group} from '../../../models/group';
import {ProductsService} from '../../../services/products.service';
import {Observable} from 'rxjs';
import {ProductMenu} from '../../../models/menu/product-menu';

@Component({
  selector: 'app-list-menu-item',
  imports: [
    MatIconModule,
    MatMenuModule,
    MatIconButton,
  ],
  templateUrl: './list-menu-item.component.html',
  styleUrl: './list-menu-item.component.css'
})
export class ListMenuItemComponent implements OnInit {

  @Input()
  group?: Group;

  products$?: Observable<ProductMenu[]>;

  constructor(private _products: ProductsService) {

  }

  ngOnInit(): void {
        this.products$ = this._products.getProducts();
    }s
}
