import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaDetailComponent } from './pa-detail.component';

describe('PaDetailComponent', () => {
  let component: PaDetailComponent;
  let fixture: ComponentFixture<PaDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
