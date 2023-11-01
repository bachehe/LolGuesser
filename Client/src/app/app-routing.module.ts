import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { WarComponent } from './war/war.component';
import { FaqComponent } from './faq/faq.component';
import { DataComponent } from './data/data.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'war', component: WarComponent},
  { path: 'faq', component: FaqComponent},
  { path: 'data', component: DataComponent},
  { path: '**', redirectTo: '', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
