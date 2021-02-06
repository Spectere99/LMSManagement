import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaNotesDetailComponent } from './pa-notes-detail.component';

describe('PaNotesDetailComponent', () => {
  let component: PaNotesDetailComponent;
  let fixture: ComponentFixture<PaNotesDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaNotesDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaNotesDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
