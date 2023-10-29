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
      transition('void => *', [animate('1s')]),
    ]),
  ],
})

export class WarComponent implements OnInit{
  champions: Character[] = [];
  winner: any;
  userWinner: string = '';
  displayedValue: boolean = false;

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
      if (this.champions[0][attribute] > this.champions[1][attribute]) {
        this.winner = this.champions[0].name;
      } else if (this.champions[0][attribute] < this.champions[1][attribute]) {
        this.winner = this.champions[1].name;
      }
      else if(this.champions[0][attribute] === this.champions[1][attribute]){
        //tbd
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
    console.log('you win');
    this.delay(1500).then(any =>{
      this.displayedValue = false;
      this.getCharacters();
    })
  }
  private onLost(): void{
    console.log('you lost');
    this.delay(1500).then(any =>{
      this.displayedValue = false;
      this.getCharacters();
    })
  }
  private async delay(ms: number) {
    await new Promise<void>(resolve => setTimeout(()=> resolve(), ms));
  }
}
