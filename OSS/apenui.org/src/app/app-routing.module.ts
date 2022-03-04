import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ZooComponent } from './zoo/zoo.component';

const routes: Routes = [
  { path: '', component: ZooComponent },
  // { path: 'apen', component: AppComponent },
  // { path: 'leeuwen', component: AppComponent },
  // { path: 'vogels', component: AppComponent },
  // { path: 'vissen', component: AppComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
