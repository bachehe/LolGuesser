import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarComponent } from './war.component';
import { WarRoutingModule } from './war-routing.module';

@NgModule({
  declarations: [WarComponent],
  imports: [
    CommonModule,
    WarRoutingModule
  ]
})
export class WarModule { }
