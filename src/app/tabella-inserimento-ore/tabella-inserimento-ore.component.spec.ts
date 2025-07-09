import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabellaInserimentoOreComponent } from './tabella-inserimento-ore.component';

describe('TabellaInserimentoOreComponent', () => {
  let component: TabellaInserimentoOreComponent;
  let fixture: ComponentFixture<TabellaInserimentoOreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabellaInserimentoOreComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TabellaInserimentoOreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
