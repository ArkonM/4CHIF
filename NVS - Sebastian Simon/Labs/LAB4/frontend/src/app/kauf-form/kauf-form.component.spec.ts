import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KaufFormComponent } from './kauf-form.component';

describe('KaufFormComponent', () => {
  let component: KaufFormComponent;
  let fixture: ComponentFixture<KaufFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ KaufFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(KaufFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
