import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortenUrlDetailsComponent } from './shorten-url-details.component';

describe('ShortenUrlDetailsComponent', () => {
  let component: ShortenUrlDetailsComponent;
  let fixture: ComponentFixture<ShortenUrlDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShortenUrlDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ShortenUrlDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
