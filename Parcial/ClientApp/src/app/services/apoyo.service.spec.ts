import { TestBed } from '@angular/core/testing';

import { ApoyoService } from './apoyo.service';

describe('ApoyoService', () => {
  let service: ApoyoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApoyoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
