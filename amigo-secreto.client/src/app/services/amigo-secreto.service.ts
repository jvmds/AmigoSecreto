import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment.development';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {User} from '../models/user';
import {Group} from '../models/group';

@Injectable({
  providedIn: 'root'
})
export class AmigoSecretoService {

  private urlService= environment.configAmigoSecretoBackend.url;

  constructor(private http: HttpClient) { }

  getUser(id: Number): Observable<User> {
    return this.http.get<User>(`${this.urlService}/users/${id}`);
  }

  getGroup(id: Number): Observable<Group> {
    //return this.http.get<Group>(`${this.urlService}/groups/${id}`);
    return this.http.get<Group>(`/groups/${id}`);
  }
}
