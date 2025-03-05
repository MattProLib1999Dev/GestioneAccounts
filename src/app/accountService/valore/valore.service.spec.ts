import { TestBed } from '@angular/core/testing';

import { ValoreService } from './valore.service';

describe('ValoreService', () => {
  let service: ValoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ValoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
