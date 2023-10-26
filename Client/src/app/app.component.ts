import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Client';
  characters: any[] = [];

  constructor(private http: HttpClient){}

  ngOnInit(): void {
    console.log(this.characters)
    this.http.get('https://localhost:7224/api/Character').subscribe({
      next: (response: any) => this.characters = response,
      error: error => console.log(error),
      complete: () => {
        console.log('completed');
        console.log('extra');
      }
    })
  }
}
