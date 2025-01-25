import {
  Component,
  Input,
  Signal,
  ViewChild
} from '@angular/core';
import {MatSidenav, MatSidenavModule} from '@angular/material/sidenav';
import {MatTreeModule} from '@angular/material/tree';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';

interface FoodNode {
  name: string;
  children?: FoodNode[];
}

const TREE_DATA: FoodNode[] = [
  {
    name: 'Fruit',
    children: [],
  },
  {
    name: 'Vegetables',
    children: [
      {
        name: 'Green',
        children: [{name: 'Broccoli'}, {name: 'Brussels sprouts'}],
      },
      {
        name: 'Orange',
        children: [{name: 'Pumpkins'}, {name: 'Carrots'}],
      },
    ],
  },
];

@Component({
  selector: 'app-side-menu',
  imports: [
    MatSidenavModule,
    MatTreeModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './side-menu.component.html',
  styleUrl: './side-menu.component.css'
})
export class SideMenuComponent {

  @Input({required: true})
  isMobile!: Signal<boolean>;

  @ViewChild(MatSidenav)
  protected sidenavBar?: MatSidenav;

  dataSource = TREE_DATA;

  childrenAccessor = (node: FoodNode) => node.children ?? [];

  hasChild = (_: number, node: FoodNode) => !!node.children && node.children.length > 0;

  toggle() {
    this.sidenavBar?.toggle();
  }
}
