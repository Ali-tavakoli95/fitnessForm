import { TestBed } from '@angular/core/testing';

import { FitnessFormService } from './fitness-form.service';

describe('FitnessFormService', () => {
  let service: FitnessFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FitnessFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
