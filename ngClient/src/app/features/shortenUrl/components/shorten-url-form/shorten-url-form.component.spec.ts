import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortenUrlFormComponent } from './shorten-url-form.component';

describe('ShortenUrlFormComponent', () => {
  let component: ShortenUrlFormComponent;
  let fixture: ComponentFixture<ShortenUrlFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShortenUrlFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ShortenUrlFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
