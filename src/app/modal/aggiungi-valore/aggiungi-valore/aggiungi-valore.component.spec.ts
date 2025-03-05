import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AggiungiValoreComponent } from './aggiungi-valore.component';

describe('AggiungiValoreComponent', () => {
  let component: AggiungiValoreComponent;
  let fixture: ComponentFixture<AggiungiValoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AggiungiValoreComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AggiungiValoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
