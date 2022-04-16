import { TestBed } from '@angular/core/testing';

import { PreventUnsavedGuard } from './prevent-unsaved.guard';

describe('PreventUnsavedGuard', () => {
  let guard: PreventUnsavedGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(PreventUnsavedGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
