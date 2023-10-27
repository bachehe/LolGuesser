import { Component, OnInit } from '@angular/core';
import { Character } from '../shared/models/character';
import { WarService } from './war.service';

@Component({
  selector: 'app-war',
  templateUrl: './war.component.html',
  styleUrls: ['./war.component.scss']
})

export class WarComponent implements OnInit{
  champions: Character[] = [];
  winner: any;
  leftHigher: boolean = false;
  rightHigher: boolean = false;
  userWinner: string = '';

  constructor(private warService: WarService){}

  ngOnInit(): void {
    this.onButtonClick();
  }

  onButtonClick(): void {
    this.warService.getWarCharacters().subscribe({
      next: response => this.champions = response,
    });


}
  private winnerCharacter(): void {
    this.leftHigher = false;
    this.rightHigher = false;
    this.winner = '';

    const attributesToCompare: (keyof Character)[] = ['hp', 'ap', 'ad', 'hpGain'];

    for (const attribute of attributesToCompare) {
      if (this.champions[0][attribute] > this.champions[1][attribute]) {
        this.leftHigher = true;
        this.winner = this.champions[0].name;
      } else if (this.champions[0][attribute] < this.champions[1][attribute]) {
        this.rightHigher = true;
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
    if(this.userWinner === this.winner){
      this.onWin();
    }
    else{
      this.onLost();
    }
  }
  private onWin(): void{
    console.log('you win');
  }
  private onLost(): void{
    console.log('you lost');
  }
}
