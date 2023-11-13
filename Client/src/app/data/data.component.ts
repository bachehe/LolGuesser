import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Character } from '../shared/models/character';
import { basicAnimation, fadeAnimation } from '../shared/animations';
import { Item } from '../shared/models/item';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.scss'],
  animations: [
    fadeAnimation, basicAnimation
  ]
})
export class DataComponent implements OnInit{
  dataDisplay: boolean = false;
  dataFrom: string = '01/11/2023';
  buttonText: string = 'Items';
  displayText: string = 'Champions';
  champions: Character[] = [];
  items: Item[] = [];

  constructor(private dataService: DataService){}

  ngOnInit(): void {
    this.getChampions();
    this.getItems();
  }

  private getChampions(): void {
    this.dataService.getCharacters().subscribe({
      next: response => this.champions = response
    })
  }

  private getItems(): void {
    this.dataService.getItems().subscribe({
      next: response => this.items = response
    })
  }
  public changeData(): boolean{
    this.buttonText = this.dataDisplay ? "Items" : "Champions";
    this.displayText = this.dataDisplay ? "Champions" : "Items";

    console.log(this.dataDisplay);
    return this.dataDisplay == false ?  this.dataDisplay = true : this.dataDisplay = false;
  }
}
