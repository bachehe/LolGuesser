import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarItemComponent } from './war-item.component';
import { WarRoutingModule } from './war-item-routing.module';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    WarItemComponent
  ],
  imports: [
    CommonModule,
    WarRoutingModule,
    SharedModule
  ]
})
export class WarItemModule { }
