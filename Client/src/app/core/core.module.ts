import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [
    NavBarComponent,
  ],
  imports: [
    RouterModule,
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  exports:[
    NavBarComponent,
  ]
})
export class CoreModule { }
