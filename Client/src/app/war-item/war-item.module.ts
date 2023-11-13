import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarItemComponent } from './war-item.component';
import { WarRoutingModule } from './war-item-routing.module';



@NgModule({
  declarations: [
    WarItemComponent
  ],
  imports: [
    CommonModule,
    WarRoutingModule
  ]
})
export class WarItemModule { }
