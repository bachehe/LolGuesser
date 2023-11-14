import { Component, OnInit } from '@angular/core';
import { MergedCharacter } from '../shared/models/mergedCharacter';
import { WarItemService } from './war-item.service';
import { catchError, map, retry, switchMap, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Character } from '../shared/models/character';

@Component({
  selector: 'app-war-item',
  templateUrl: './war-item.component.html',
  styleUrls: ['./war-item.component.scss']
})

export class WarItemComponent implements OnInit {
  champions$: Observable<{ item: string[], itemPictureUrl: string[], character: Character[] }> | undefined;
  warChampions: MergedCharacter[] = [];

  items: string[] = [];
  itemPictureUrls: string[] = [];
  characters: Character[] = [];

  constructor(private warService: WarItemService){
  }

  ngOnInit(): void {

  this.getData();

  }
  private getData(): void{
    this.warService.getWarCharacters().subscribe(response => {
      this.items = response.item;
      this.itemPictureUrls = response.itemPictureUrl;
      this.characters = response.character;
  });
  }
}
