import { Injectable } from '@angular/core';
import { Character } from '../shared/models/character';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl = 'https://localhost:7224/api/'
  constructor(private http: HttpClient) { }

  getCharacters(){
    return this.http.get<Character[]>(this.baseUrl + 'Character')
  }
}
