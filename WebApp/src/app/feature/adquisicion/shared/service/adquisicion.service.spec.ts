import { TestBed } from '@angular/core/testing';

import { AdquisicionService } from './adquisicion.service';

describe('AdquisicionService', () => {
  let service: AdquisicionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdquisicionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
