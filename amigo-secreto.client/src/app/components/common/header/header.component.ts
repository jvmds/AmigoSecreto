import {Component, EventEmitter, Input, OnInit, Output, signal, Signal} from '@angular/core';
import {MatToolbar} from '@angular/material/toolbar';
import {MatIcon} from '@angular/material/icon';
import {MatIconButton} from '@angular/material/button';

@Component({
  selector: 'app-header',
  imports: [
    MatToolbar,
    MatIcon,
    MatIconButton
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  @Input({required: true})
  isMobile!: Signal<boolean>;

  @Output()
  clickOnMenuButtonEvent = new EventEmitter<void>();

  protected clickOnMenuButton() {
    this.clickOnMenuButtonEvent.emit();
  }
}
