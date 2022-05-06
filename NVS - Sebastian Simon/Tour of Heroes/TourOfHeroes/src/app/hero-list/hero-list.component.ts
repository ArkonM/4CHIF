import { Component, OnInit } from '@angular/core';
import {HEROES} from "../mock-heroes";

@Component({
  selector: 'app-hero-list',
  templateUrl: './hero-list.component.html',
  styleUrls: ['./hero-list.component.scss']
})
export class HeroListComponent implements OnInit {

  heroes = HEROES;

  constructor() { }

  ngOnInit(): void {
    console.log("Servus")
  }

}
