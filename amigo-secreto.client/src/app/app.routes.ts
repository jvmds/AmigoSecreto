import { Routes } from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {SearchGroupComponent} from './components/groups/search-group/search-group.component';


export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: "groups", component: SearchGroupComponent}
];
