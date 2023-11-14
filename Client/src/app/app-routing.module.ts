import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FaqComponent } from './faq/faq.component';
import { DataComponent } from './data/data.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'war', loadChildren: () => import('./war/war.module').then(m => m.WarModule)},
  { path: 'war-item', loadChildren: () => import('./war-item/war-item.module').then(m => m.WarItemModule)},
  { path: 'faq', component: FaqComponent},
  { path: 'data', component: DataComponent},
  { path: '**', redirectTo: '', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
