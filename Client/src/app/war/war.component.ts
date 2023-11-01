import { Component, ElementRef, OnInit } from '@angular/core';
import { Character } from '../shared/models/character';
import { WarService } from './war.service';
import { basicAnimation, fadeAnimation, fadeOutAnimation, imageAnimation } from '../shared/animations';

@Component({
  selector: 'app-war',
  templateUrl: './war.component.html',
  styleUrls: ['./war.component.scss'],
  animations: [
    fadeOutAnimation, imageAnimation, basicAnimation, fadeAnimation
  ],
})

export class WarComponent implements OnInit{
  fadeState: string = 'visible';
  champions: Character[] = [];
  winner: any;
  lostText: string = '';
  userWinner: string = '';
  animateState: string = 'void';

  loading: boolean = false;
  displayedValue: boolean = false;
  cricleX: boolean = false;
  lost: boolean = false;
  win: boolean = false;
  isEqual: boolean = false;

  currentScore: number = 0;
  highScore: number = 0;

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

  constructor(private warService: WarService){
  }

  ngOnInit(): void {
    this.getCharacters();
  }

  getValue(champion: Character, key: string): any {
    return champion[key as keyof Character];
  }

  private fadeOut(): void {
    this.fadeState = 'invisible';
  }
  getCharacters(): void {
    if (this.loading) {
      return;
    }
    this.loading = true;
    this.animateState = 'reset';
    setTimeout(() => {
      this.animateState = 'enter';
    }, 0);

    this.warService.getWarCharacters().subscribe({
      next: response => {
        this.champions = response;
        this.loading = false;
      },
      error: err => {
        console.error('Error fetching characters:', err);
        this.loading = false;
      }
    });
    this.fadeState ='visible'
  }

  private winnerCharacter(): void {
    this.winner = '';
    const attributesToCompare: (keyof Character)[] = ['hp', 'ms', 'ad', 'hpGain', 'mana' ,'manaGain', 'as', 'armor', 'armorGain', 'mr', 'range'];
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

    this.win = true;
    this.currentScore++;
    this.delay(1500).then(any =>{
      this.loading = false;
      this.displayedValue = false;
      this.isEqual = false;
      this.win = false;
      this.fadeOut();
      this.delay(1500).then(any => {
        this.getCharacters();
      })
    })
  }
  private onLost(): void{
    this.cricleX = true;
    this.delay(1500).then(any => {
      this.lost = true;
      this.loading = false;
      this.LostTextPicker();
      if(this.currentScore > this.highScore){
        this.highScore = this.currentScore;
        localStorage.setItem('session', JSON.stringify(this.highScore));
      }
    })
  }

  tryAgain(): void{
    this.cricleX = false;
    this.currentScore = 0;
    this.displayedValue = false;
    this.lost = false;
    this.getCharacters();
  }
  private async delay(ms: number) {
    await new Promise<void>(resolve => setTimeout(()=> resolve(), ms));
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
}
