import { Component } from '@angular/core';
import { fadeAnimation } from '../shared/animations';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.component.html',
  styleUrls: ['./faq.component.scss'],
  animations: [
    fadeAnimation
  ]
})
export class FaqComponent {

}
