import { Component, Input, OnInit } from '@angular/core';
import { WarComponent } from 'src/app/war/war.component';

@Component({
  selector: 'app-war-lost',
  templateUrl: './war-lost.component.html',
  styleUrls: ['./war-lost.component.scss']
})
export class WarLostComponent implements OnInit{
  @Input() lost?: boolean;
  @Input() lostText?: string ;
  @Input() highScore?: number;
  @Input() currentScore: number = 0;

  constructor(private component:WarComponent){}
  cricleX = false;
  displayedValue = false;

  ngOnInit(): void {
    this.LostTextPicker();
    this.lost = true;
  }

  triggerTryAgain(): void {
    this.component.tryAgain();
    this.lost = false;
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
