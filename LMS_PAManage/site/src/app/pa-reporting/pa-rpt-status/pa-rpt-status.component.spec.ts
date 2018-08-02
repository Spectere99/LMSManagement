import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaRptStatusComponent } from './pa-rpt-status.component';

describe('PaRptStatusComponent', () => {
  let component: PaRptStatusComponent;
  let fixture: ComponentFixture<PaRptStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaRptStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaRptStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
