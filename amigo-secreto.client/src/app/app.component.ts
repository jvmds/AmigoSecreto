import {Component} from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import {HeaderComponent} from './components/common/header/header.component';
import {RouterOutlet} from '@angular/router';


@Component({
  selector: 'app-root',
  imports: [
    MatGridListModule,
    HeaderComponent,
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'amigo-secreto.client';
  constructor() {}
}
