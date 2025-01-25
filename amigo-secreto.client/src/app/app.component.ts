import {Component} from '@angular/core';
import {RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatGridListModule } from '@angular/material/grid-list';
import {GroupListComponent} from './components/groups/group-list/group-list.component';
import {HeaderComponent} from './components/common/header/header.component';
import {MainComponent} from './components/common/main/main.component';


@Component({
  selector: 'app-root',
  imports: [RouterLink, RouterLinkActive, RouterOutlet, MatGridListModule, GroupListComponent, HeaderComponent, MainComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'amigo-secreto.client';
  constructor() {}
}
