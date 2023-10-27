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

  userWinner: string = '';
  displayedValue: boolean = false;

  constructor(private warService: WarService){}

  ngOnInit(): void {
    //this.onButtonClick();
  }

  onButtonClick(): void {
    this.warService.getWarCharacters().subscribe({
      next: response => this.champions = response,
    });
}
  private winnerCharacter(): void {
    this.winner = '';

    const attributesToCompare: (keyof Character)[] = ['hp', 'ap', 'ad', 'hpGain'];

    for (const attribute of attributesToCompare) {
      if (this.champions[0][attribute] > this.champions[1][attribute]) {
        this.winner = this.champions[0].name;
      } else if (this.champions[0][attribute] < this.champions[1][attribute]) {
        this.winner = this.champions[1].name;
      }
      // else if(this.champions[0][attribute] === this.champions[1][attribute]){
      //   this.onWin();
      // }
      if (this.winner) {
        break;
      }
    }
  }

  //on click schema:
  //choose character
  //validate result
  //show hp
  //select next
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
    this.displayedValue = false;
    this.delay(1500).then(any =>{
      this.onButtonClick();
    })
  }
  private onLost(): void{
    console.log('you lost');
    this.displayedValue = false;
    this.delay(1500).then(any =>{
      this.onButtonClick();
    })
  }
  private async delay(ms: number) {
    await new Promise<void>(resolve => setTimeout(()=> resolve(), ms));
  }
}
