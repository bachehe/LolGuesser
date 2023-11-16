import { Component, OnInit, ViewChild } from '@angular/core';
import { MergedCharacter } from '../shared/models/mergedCharacter';
import { WarItemService } from './war-item.service';
import { Observable} from 'rxjs';
import { Character } from '../shared/models/character';
import { basicAnimation, fadeAnimation, fadeOutAnimation, imageAnimation } from '../shared/animations';
import { WarLostComponent } from '../shared/war-lost/war-lost.component';

@Component({
  selector: 'app-war-item',
  templateUrl: './war-item.component.html',
  styleUrls: ['./war-item.component.scss'],
  animations: [
    fadeOutAnimation, imageAnimation, basicAnimation, fadeAnimation
  ],
})

export class WarItemComponent implements OnInit {
  @ViewChild(WarLostComponent) lostStatusComponent!: WarLostComponent;

  champions$: Observable<{ item: string[], itemPictureUrl: string[], character: Character[] }> | undefined;
  warChampions: MergedCharacter[] = [];

  items: string[] = [];
  itemPictureUrls: string[] = [];
  champions: Character[] = [];
  lostText: string = '';

  userWinner: string = '';
  animateState: string = 'void';
  loading: boolean = false;
  fadeState: string = 'visible';
  cricleX: boolean = false;
  lost: boolean = false;
  displayedValue: boolean = false;
  currentScore: number = 0;
  highScore: number = 0;
  win: boolean = false;

  winner: any;
  isEqual: boolean = false;


  championAttributes = [
    { key: 'hp', label: 'hp' },
    { key: 'ad', label: 'attack damage' },
    { key: 'ms', label: 'movement speed' },
    { key: 'mana', label: 'mana attribute' },
    { key: 'manaGain', label: 'mana gain' },
    { key: 'as', label: 'attack speed' },
    { key: 'armor', label: 'armor' },
    { key: 'armorGain', label: 'armor gain' },
    { key: 'mr', label: 'magic resists' },
    { key: 'hpGain', label: 'hp gain' },
    { key: 'range', label: 'attack range' }
];

  constructor(private warService: WarItemService){
  }

  ngOnInit(): void {
    this.getData();
  }
  getValue(champion: Character, key: string): any {
    return champion[key as keyof Character];
  }

  private fadeOut(): void {
    this.fadeState = 'invisible';
  }
  private fadeIn(): void{
    this.fadeState = 'visible';

  }
  private getData(): void{
    this.fadeIn();

    if (this.loading) {
      return;
    }
    this.loading = true;
    this.animateState = 'reset';
      setTimeout(() => {
        this.animateState = 'enter';
      }, 0);

      this.warService.getWarCharacters().subscribe(response => {
        this.items = response.item;
        this.itemPictureUrls = response.itemPictureUrl;
        this.champions = response.character;
        this.loading = false;
        }
      );

  }

  private winnerCharacter(): void {
    this.winner = '';
    const attributesToCompare: (keyof Character)[] = ['hp', 'ms', 'ad',  'mana', 'as', 'armor',  'mr'];
    for (const attribute of attributesToCompare) {
      if(+this.champions[0][attribute] > 0){
        if(this.champions[0][attribute] === this.champions[1][attribute]){
          this.isEqual = true;
        }
      }

      if (this.champions[0][attribute] > this.champions[1][attribute]) {
        this.winner = this.champions[0].name;
      } else if (this.champions[0][attribute] < this.champions[1][attribute]) {
        this.winner = this.champions[1].name;
      }
      if (this.winner) {
        break;
      }
    }
  }
  userChoice(side: string): void{
    this.loading = true;
    this.winnerCharacter();
    if(side === 'left'){
      this.userWinner = this.champions[0].name;
    }
    else if(side ==='right'){
      this.userWinner = this.champions[1].name;
    }

    this.delay(300).then(any => {
      this.displayedValue = true;
      this.delay(1500).then(any =>{
        if(this.isEqual === true){
          this.onWin();
          return;
        }
        if(this.userWinner === this.winner){
          this.onWin();
          }
        else{
          this.onLost();
        }
        })
      })
  }
  private onWin(): void{
    console.log(this.highScore);
    this.win = true;
    this.currentScore++;
    this.delay(1500).then(any =>{
      this.fadeOut();
      this.loading = false;
      this.displayedValue = false;
      this.isEqual = false;
      this.win = false;
      this.delay(1500).then(any => {
        this.getData();
      })
    })
  }
  private onLost(): void{
    this.LostTextPicker();
    this.cricleX = true;
    this.delay(1500).then(any => {
      this.lost = true;
      this.loading = false;
      if(this.currentScore > this.highScore){
        this.highScore = this.currentScore;
        localStorage.setItem('sessionItem', JSON.stringify(this.highScore));
      }
    })
    let score = localStorage.getItem('sessionItem');
    this.highScore = score !== null ? Number(score) : this.highScore;
  }

  private LostTextPicker(): void{
    switch (true) {
      case this.currentScore === 0:
        this.lostText = "I could not be more disappointed";
        break;
      case this.currentScore > 0 && this.currentScore <= 2:
        this.lostText = "You have failed me.";
        break;
      case this.currentScore > 2 && this.currentScore <= 4:
        this.lostText = "OOPSIE WOOPSIE";
        break;
      case this.currentScore > 4 && this.currentScore <= 6:
        this.lostText = "WOW";
        break;
      default:
        this.lostText = "how?????????";
        break;
    }
  }

  tryAgain(): void{
    this.cricleX = false;
    this.currentScore = 0;
    this.displayedValue = false;
    this.lost = false;
    this.getData();
  }
  private async delay(ms: number) {
   await new Promise<void>(resolve => setTimeout(()=> resolve(), ms));
  }
}
