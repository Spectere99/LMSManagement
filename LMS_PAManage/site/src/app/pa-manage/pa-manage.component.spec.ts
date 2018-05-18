import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaManageComponent } from './pa-manage.component';

describe('PaManageComponent', () => {
  let component: PaManageComponent;
  let fixture: ComponentFixture<PaManageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaManageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
