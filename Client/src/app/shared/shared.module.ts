import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { WarItemH1Component } from './war-h1/war-item-h1.component';
import { WarLostComponent } from './war-lost/war-lost.component';



@NgModule({
  declarations: [
    FooterComponent,
    WarItemH1Component,
    WarLostComponent
  ],
  imports: [
    CommonModule,
  ],
  exports:[
    FooterComponent,
    WarItemH1Component,
    WarLostComponent
  ]
})
export class SharedModule { }
