import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ZooComponent } from './zoo.component';

describe('ZooComponent', () => {
  let component: ZooComponent;
  let fixture: ComponentFixture<ZooComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ZooComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ZooComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
