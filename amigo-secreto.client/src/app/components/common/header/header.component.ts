import {Component, EventEmitter, inject, Input, OnDestroy, OnInit, Output, signal, Signal} from '@angular/core';
import {MatToolbar} from '@angular/material/toolbar';
import {MatIcon, MatIconModule} from '@angular/material/icon';
import {MatButtonModule, MatIconButton} from '@angular/material/button';
import {AsyncPipe} from "@angular/common";
import {CapitalizePipe} from "../../../utils/capitalize.pipe";
import {MatSidenav, MatSidenavContainer, MatSidenavContent} from "@angular/material/sidenav";
import {MatTree, MatTreeNode, MatTreeNodeDef, MatTreeNodePadding, MatTreeNodeToggle} from "@angular/material/tree";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {ProductMenu} from '../../../models/menu/product-menu';
import {Observable} from 'rxjs';
import {ProductsService} from '../../../services/products.service';
import {MediaMatcher} from '@angular/cdk/layout';
import {MatDividerModule} from '@angular/material/divider';

@Component({
  selector: 'app-header',
  imports: [
    MatToolbar,
    MatIcon,
    MatIconButton,
    AsyncPipe,
    CapitalizePipe,
    MatSidenav,
    MatSidenavContainer,
    MatSidenavContent,
    MatTree,
    MatTreeNode,
    MatTreeNodeDef,
    MatTreeNodePadding,
    MatTreeNodeToggle,
    RouterLink,
    RouterLinkActive,
    MatButtonModule,
    MatDividerModule,
    MatIconModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit, OnDestroy {

  products$?: Observable<ProductMenu[]>;

  protected readonly isMobile = signal(true);
  private readonly _mobileQuery: MediaQueryList;
  private readonly _mobileQueryListener: () => void;

  constructor(private productsService: ProductsService) {
    const media = inject(MediaMatcher);
    this._mobileQuery = media.matchMedia("(max-width: 600px)");
    this.isMobile.set(this._mobileQuery.matches);
    this._mobileQueryListener = () => this.isMobile.set(this._mobileQuery.matches);
    this._mobileQuery.addEventListener("change", this._mobileQueryListener);
  }

  childrenAccessor(product: ProductMenu): ProductMenu[] {
    return product.byProducts;
  }

  hasChild(_: number, product: ProductMenu): boolean {
    return product.byProducts.length > 0
  }

  ngOnInit(): void {
    this.products$ = this.productsService.getProducts();
  }

  ngOnDestroy(): void {
    this._mobileQuery.removeEventListener("change", this._mobileQueryListener);
  }
}
