import {Component, OnInit} from '@angular/core';
import {UserService} from '../../../services/user.service';
import {Observable} from 'rxjs';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatListModule} from '@angular/material/list';
import {MatDividerModule} from '@angular/material/divider';
import {AsyncPipe} from '@angular/common';
import {Group} from '../../../models/group';
import {ListMenuItemComponent} from '../../common/list-menu-item/list-menu-item.component';


@Component({
  selector: 'app-group-list',
  imports: [
    MatCardModule,
    MatIconModule,
    MatMenuModule,
    MatButtonModule,
    MatGridListModule,
    MatListModule,
    MatDividerModule,
    AsyncPipe,
    ListMenuItemComponent,
  ],
  templateUrl: './group-list.component.html',
  styleUrl: './group-list.component.css'
})
export class GroupListComponent implements OnInit {

  group$: Observable<Group> | undefined;

  constructor(private userService: UserService) {
  }

  ngOnInit(): void {
    this.group$ = this.userService.getGroup(1);
  }
}
