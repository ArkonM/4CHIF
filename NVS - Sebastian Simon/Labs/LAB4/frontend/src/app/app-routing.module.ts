import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {ShopComponent} from "./shop/shop.component";
import {WarenkorbComponent} from "./warenkorb/warenkorb.component";
import {KaufFormComponent} from "./kauf-form/kauf-form.component";

const routes: Routes = [
  {path: '', component: HomeComponent}
, {path: 'Shop', component: ShopComponent}
, {path: 'Warenkorb', component: WarenkorbComponent}
, {path: 'KaufForm', component: KaufFormComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
