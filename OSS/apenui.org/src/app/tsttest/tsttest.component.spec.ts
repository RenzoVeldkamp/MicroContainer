import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TsttestComponent } from './tsttest.component';

describe('TsttestComponent', () => {
  let component: TsttestComponent;
  let fixture: ComponentFixture<TsttestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TsttestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TsttestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
