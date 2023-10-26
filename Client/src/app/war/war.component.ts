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

  constructor(private warService: WarService){}

  ngOnInit(): void {

    this.warService.getWarCharacters().subscribe({
      next: response => this.champions = response,
    });
  }
}
