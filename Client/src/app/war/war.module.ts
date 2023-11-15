import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarComponent } from './war.component';
import { WarRoutingModule } from './war-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [WarComponent],
  imports: [
    CommonModule,
    WarRoutingModule,
    SharedModule
  ]
})
export class WarModule { }
