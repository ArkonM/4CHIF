import { Component, OnInit } from '@angular/core';
import {WarenkorbItem} from "../Service/WarenkorbItem";
import {Router} from "@angular/router";

@Component({
  selector: 'app-warenkorb',
  templateUrl: './warenkorb.component.html',
  styleUrls: ['./warenkorb.component.css']
})
export class WarenkorbComponent implements OnInit {

  constructor(
    private router:Router
  ) { }

  items : WarenkorbItem[] = [];
  itemsAdded : WarenkorbItem [] = [];
  summe : number = 0;

  ngOnInit(): void {
    this.items = JSON.parse(sessionStorage.getItem('WarenkorbItems')!);
    if (!this.items) {
      this.items = [];
    }

    for (let i of this.items) {
      this.summe += (i.count * i.preis);
    }
  }

  delete(WItem: WarenkorbItem) : void {
    console.log("All Items deleted with id: " + WItem.id);
    this.items = JSON.parse(sessionStorage.getItem('WarenkorbItems')!);
    let i = 0;

    while (i < this.items.length) {
      if (this.items[i].id != WItem.id) {
        this.itemsAdded.push(this.items[i]);
      }
      i++;
    }

    sessionStorage.setItem('WarenkorbItems', JSON.stringify(this.itemsAdded));
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/Warenkorb']);
    });

  }

  navigateKauf() {
    this.router.navigate(['/KaufForm']);
  }
}
