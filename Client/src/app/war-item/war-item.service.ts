import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MergedCharacter } from '../shared/models/mergedCharacter';
import { Observable } from 'rxjs';
import { Character } from '../shared/models/character';

@Injectable({
  providedIn: 'root'
})
export class WarItemService {
  baseUrl = 'https://localhost:7224/api/'
  constructor(private http: HttpClient) {}

  getChampions(): Observable<{ item: string[], itemPictureUrl: string[], character: Character[] }> {
    return this.http.get<{ item: string[], itemPictureUrl: string[], character: Character[] }>(this.baseUrl + 'Character/item-war');
  }
  getWarCharacters(){
    return this.http.get<any>(this.baseUrl + 'Character/item-war')
  }
}
