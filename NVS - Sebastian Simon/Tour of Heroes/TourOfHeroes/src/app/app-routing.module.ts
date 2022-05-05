import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HeroListComponent} from "./hero-list/hero-list.component";
import {HeroesComponent} from "./heroes/heroes.component";

const routes: Routes = [
  { path: 'list', component: HeroListComponent },
  { path: '', component: HeroesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
