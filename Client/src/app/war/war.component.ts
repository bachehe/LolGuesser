import { Component, ElementRef, OnInit } from '@angular/core';
import { Character } from '../shared/models/character';
import { WarService } from './war.service';
import { AnimationBuilder, animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-war',
  templateUrl: './war.component.html',
  styleUrls: ['./war.component.scss'],
  animations: [
    trigger('anim', [
      state('void', style({ opacity: 0, transform: 'scale(0.8)' })),
      state('*', style({ opacity: 1, transform: 'scale(1)' })),
      transition('void => *', [animate('.5s')]),
    ]),
  ],
})

export class WarComponent implements OnInit{
  champions: Character[] = [];
  winner: any;
  userWinner: string = '';
  displayedValue: boolean = false;

  isEqual: boolean = false;

  currentScore: number = 0;
  highScore: number = 0;

  constructor(private warService: WarService, private animationBuilder: AnimationBuilder, private elementRef: ElementRef){}

  ngOnInit(): void {
  }

  getCharacters(): void {
    this.warService.getWarCharacters().subscribe({
      next: response => this.champions = response,
    });
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
          this.displayedValue = true;
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
    this.currentScore++;
    console.log('you win');
    this.delay(1500).then(any =>{
      this.displayedValue = false;
      this.isEqual = false;
      this.getCharacters();
    })
  }
  private onLost(): void{
    if(this.currentScore > this.highScore){
      this.highScore = this.currentScore;
      localStorage.setItem('session', JSON.stringify(this.highScore));
    }

    let data = localStorage.getItem('session');
    alert(data)
    console.log('you lost')
    this.delay(1500).then(any =>{
      this.currentScore = 0;
      this.displayedValue = false;
      this.getCharacters();
    })
  }
  private async delay(ms: number) {
    await new Promise<void>(resolve => setTimeout(()=> resolve(), ms));
  }
}
