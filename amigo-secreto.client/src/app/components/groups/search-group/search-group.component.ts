import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {Group} from '../../../models/group';
import {AmigoSecretoService} from '../../../services/amigo-secreto.service';
import {AsyncPipe} from '@angular/common';
import {MatGridList} from '@angular/material/grid-list';
import {MatMenu, MatMenuItem, MatMenuTrigger} from '@angular/material/menu';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-search-group',
  imports: [
    AsyncPipe,
    MatGridList,
    MatMenu,
    MatMenuItem,
    MatMenuTrigger,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatIconModule


  ],
  templateUrl: './search-group.component.html',
  styleUrl: './search-group.component.css'
})
export class SearchGroupComponent implements OnInit {

  group$?: Observable<Group>;
  value = 'Clear me';

  constructor(private _amigoSecretoService: AmigoSecretoService) {
  }

  ngOnInit(): void {
    this.group$ = this._amigoSecretoService.getGroup(1);
  }
}
