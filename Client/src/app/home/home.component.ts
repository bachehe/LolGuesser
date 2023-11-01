import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { fadeAnimation } from '../shared/animations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  animations: [ fadeAnimation ],

})
export class HomeComponent {

}
