import { Component, OnInit } from '@angular/core';
import { MergedCharacter } from '../shared/models/mergedCharacter';
import { WarItemService } from './war-item.service';
import { Observable, of } from 'rxjs';
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

  championAttributes = [
    { key: 'hp', label: 'hp?' },
    { key: 'ad', label: 'attack damage?' },
    { key: 'ms', label: 'movement speed?' },
    { key: 'mana', label: 'mana attribute?' },
    { key: 'manaGain', label: 'mana gain?' },
    { key: 'as', label: 'attack speed?' },
    { key: 'armor', label: 'armor?' },
    { key: 'armorGain', label: 'armor gain?' },
    { key: 'mr', label: 'magic resists?' },
    { key: 'hpGain', label: 'hp gain?' },
    { key: 'range', label: 'attack range?' }
];

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
