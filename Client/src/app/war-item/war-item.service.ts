import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MergedCharacter } from '../shared/models/mergedCharacter';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WarItemService {
  baseUrl = 'https://localhost:7224/api/'
  constructor(private http: HttpClient) {}

  // getChampions(): Observable<MergedCharacter[]> {
  //   // Should return an Observable of an array, not a single object
  //   return this.http.get<MergedCharacter[]>(this.baseUrl + 'Character/item-war')
  // }
  getChampions(): Observable<{ item: string[], itemPictureUrl: string[], character: MergedCharacter[] }> {
    return this.http.get<{ item: string[], itemPictureUrl: string[], character: MergedCharacter[] }>(this.baseUrl + 'Character/item-war');
  }
}
