import { TestBed } from '@angular/core/testing';

import { RedarborBaseService } from './redarbor-base-service';

describe('RedarborBaseService', () => {
  let service: RedarborBaseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RedarborBaseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
