import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaRptUserStatusComponent } from './pa-rpt-user-status.component';

describe('PaRptUserStatusComponent', () => {
  let component: PaRptUserStatusComponent;
  let fixture: ComponentFixture<PaRptUserStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaRptUserStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaRptUserStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
