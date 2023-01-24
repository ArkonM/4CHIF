import { Component, OnInit } from '@angular/core';
import { Product } from "../Service/Product";
import { ProductService } from "../Service/product.service";
import {WarenkorbItem} from "../Service/WarenkorbItem";

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {

  products! : Product[];

  itemList : WarenkorbItem[] = [];

  constructor(private productService:ProductService) { }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(
      data => {this.products = data},
      error => console.error("getProducts() failed")
    );
  }

  addItem(prod : Product){

    if (!this.newItem(prod)) {
      for (let WItem of this.itemList) {
        if (WItem.id == prod.id) {
          WItem.count++;
        }
      }
    } else {
      let item : WarenkorbItem = new WarenkorbItem();
      item.id = prod.id;
      item.productName = prod.productName;
      item.preis = prod.preis;
      item.count = 1;

      console.log("Added Item with id: " + prod.id);
      this.itemList.push(item);
    }

    sessionStorage.setItem('WarenkorbItems', JSON.stringify(this.itemList));

  }

  private newItem(prod : Product) : boolean {

    for (let WItem of this.itemList) {
      if (WItem.id == prod.id) {
        return false;
      }
    }

    return true;
  }
}
