import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { WarItemH1Component } from './war-item-h1/war-item-h1.component';



@NgModule({
  declarations: [
    FooterComponent,
    WarItemH1Component
  ],
  imports: [
    CommonModule,
  ],
  exports:[
    FooterComponent,
    WarItemH1Component
  ]
})
export class SharedModule { }
