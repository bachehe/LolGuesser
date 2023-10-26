import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarComponent } from './war.component';

@NgModule({
  declarations: [WarComponent],
  imports: [
    CommonModule,
  ],
  exports: [
    WarComponent
  ]
})
export class WarModule { }
