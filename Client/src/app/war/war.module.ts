import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarComponent } from './war.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [WarComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    WarComponent
  ]
})
export class WarModule { }
