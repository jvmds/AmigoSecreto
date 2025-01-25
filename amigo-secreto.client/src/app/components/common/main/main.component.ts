import {Component, inject, OnDestroy, OnInit, signal} from '@angular/core';
import {MediaMatcher} from '@angular/cdk/layout';
import {SideMenuComponent} from '../side-menu/side-menu.component';
import {HeaderComponent} from '../header/header.component';

@Component({
  selector: 'app-main',
  imports: [
    SideMenuComponent,
    HeaderComponent
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent implements OnDestroy {

  protected readonly isMobile = signal(true);
  private readonly _mobileQuery: MediaQueryList;
  private readonly _mobileQueryListener: () => void;

  constructor() {
    const media = inject(MediaMatcher);
    this._mobileQuery = media.matchMedia("(max-width: 600px)");
    this.isMobile.set(this._mobileQuery.matches);
    this._mobileQueryListener = () => this.isMobile.set(this._mobileQuery.matches);
    this._mobileQuery.addEventListener("change", this._mobileQueryListener);
  }

  ngOnDestroy(): void {
    this._mobileQuery.removeEventListener("change", this._mobileQueryListener);
  }
}
