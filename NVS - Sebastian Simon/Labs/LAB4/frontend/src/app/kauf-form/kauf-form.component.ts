import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {OrderRequestModel} from "../model/oderRequest.model";
import {OrderService} from "../Service/order.service";
import {ProductValue} from "../model/productValue.model";
import {Product} from "../model/product.model";

@Component({
  selector: 'app-kauf-form',
  templateUrl: './kauf-form.component.html',
  styleUrls: ['./kauf-form.component.css']
})
export class KaufFormComponent implements OnInit {

  orderRequest: OrderRequestModel = new OrderRequestModel();
  prodValue: ProductValue = new ProductValue();


  constructor(
    private router: Router,
    private orderService: OrderService

  ) { }

  ngOnInit(): void {
  }

  abbruch() {
    this.router.navigate(['/Warenkorb']);
  }

  sendRequest() {
    if(this.orderRequest.anrede != "" || this.orderRequest.vorname != "" || this.orderRequest.name != "" || this.orderRequest.strasse != "" || this.orderRequest.plz != "" || this.orderRequest.ort != ""){
      var items = JSON.parse(sessionStorage.getItem('WarenkorbItems')!);
      if (!items) {
        items = [];
      }

      if(items != []){
        for(let item of items){
          this.orderRequest.product.push({product: {id: item.id}, anzahl: item.count});
        }

        this.orderService.addOrder(this.orderRequest).subscribe(
          {next: () => {
            sessionStorage.removeItem('WarenkorbItems');
            this.router.navigate(['/'])
          }}
        );
      }
    }
  }
}
