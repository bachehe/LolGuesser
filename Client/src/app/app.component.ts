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
  constructor(){}

  ngOnInit(): void {
  }
}
