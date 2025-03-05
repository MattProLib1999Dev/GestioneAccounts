import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AggiungiAccountComponent } from './aggiungi-account.component';

describe('AggiungiAccountComponent', () => {
  let component: AggiungiAccountComponent;
  let fixture: ComponentFixture<AggiungiAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AggiungiAccountComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AggiungiAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
