import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AnimalComponent } from './animal/animal.component';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ZooComponent } from './zoo/zoo.component';

import { AnimalService } from './services/animal.service';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    ZooComponent,
    AnimalComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule
  ],
  providers: [
    AnimalService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
