import { Component, OnInit } from '@angular/core';
import { AnimalService } from '../services/animal.service';
import { Animal } from '../animal/animal';

@Component({
  selector: 'app-zoo',
  templateUrl: './zoo.component.html',
  styleUrls: ['./zoo.component.scss']
})
export class ZooComponent implements OnInit {
public animals:Array<Animal>;

  constructor(private animalService: AnimalService) {
    this.animals = new Array<Animal>();
   }

  ngOnInit() {
    // this.animalService.getAnimals().subscribe()
  }

}
