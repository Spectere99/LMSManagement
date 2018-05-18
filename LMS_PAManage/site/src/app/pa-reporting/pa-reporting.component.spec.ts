import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaReportingComponent } from './pa-reporting.component';

describe('PaReportingComponent', () => {
  let component: PaReportingComponent;
  let fixture: ComponentFixture<PaReportingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaReportingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
