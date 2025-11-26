import { Component, Input, OnInit } from '@angular/core';
import { Animal } from './animal';

@Component({
    selector: 'app-animal',
    templateUrl: './animal.component.html',
    styleUrls: ['./animal.component.scss'],
    standalone: false
})
export class AnimalComponent implements OnInit {
@Input() public animal:Animal;

  constructor() {
    this.animal = new Animal(0, 'name', 'species');
   }

  ngOnInit() {
  }

}
