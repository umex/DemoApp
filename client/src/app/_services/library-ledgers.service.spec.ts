import { TestBed } from '@angular/core/testing';

import { LibraryLedgersService } from './library-ledgers.service';

describe('LibraryLedgersService', () => {
  let service: LibraryLedgersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LibraryLedgersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
