import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Character } from '../shared/models/character';
import { basicAnimation, fadeAnimation } from '../shared/animations';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.scss'],
  animations: [
    fadeAnimation, basicAnimation
  ]
})
export class DataComponent implements OnInit{
  dataFrom: string = '01/11/2023';
  champions: Character[] = [];

  constructor(private dataService: DataService){}

  ngOnInit(): void {
    this.getChampions();
  }

  private getChampions(): void {
    this.dataService.getCharacters().subscribe({
      next: response => this.champions = response
    })
  }


}
