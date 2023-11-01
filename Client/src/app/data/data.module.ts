import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataComponent } from './data.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    DataComponent
  ],
  imports: [
    RouterModule,
    CommonModule
  ],
  exports: [
    DataComponent
  ]
})
export class DataModule { }
