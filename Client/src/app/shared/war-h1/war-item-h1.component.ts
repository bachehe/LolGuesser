import { Component, Input } from '@angular/core';
import { Character } from '../models/character';

@Component({
  selector: 'app-war-item-h1',
  templateUrl: './war-item-h1.component.html',
  styleUrls: ['./war-item-h1.component.scss']
})
export class WarItemH1Component {
  @Input() champions: Character[] = [];
  @Input() championAttributes = [
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

  getValue(champion: Character, key: string): any {
    return champion[key as keyof Character];
  }
}
