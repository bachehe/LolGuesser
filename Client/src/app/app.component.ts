import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Character } from './shared/models/character';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Client';
  characters: Character[] = [];

  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get<Character[]>('https://localhost:7224/api/Character').subscribe({
      next: response => this.characters = response,
      error: error => console.log(error),
    })
  }
}
