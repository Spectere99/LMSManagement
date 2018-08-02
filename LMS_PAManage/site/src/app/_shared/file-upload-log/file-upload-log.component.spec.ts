import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FileUploadLogComponent } from './file-upload-log.component';

describe('FileUploadLogComponent', () => {
  let component: FileUploadLogComponent;
  let fixture: ComponentFixture<FileUploadLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FileUploadLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FileUploadLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
