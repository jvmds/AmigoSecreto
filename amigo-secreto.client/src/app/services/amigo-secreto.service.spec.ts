import { TestBed } from '@angular/core/testing';

import { AmigoSecretoService } from './amigo-secreto.service';

describe('AmigoSecretoService', () => {
  let service: AmigoSecretoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AmigoSecretoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
