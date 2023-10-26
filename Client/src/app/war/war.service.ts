import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Character } from '../shared/models/character';

@Injectable({
  providedIn: 'root'
})
export class WarService {
  baseUrl = 'https://localhost:7224/api/'
  constructor(private http: HttpClient) { }

  getWarCharacters(){
    return this.http.get<Character[]>(this.baseUrl + 'Character/war')
  }
}
