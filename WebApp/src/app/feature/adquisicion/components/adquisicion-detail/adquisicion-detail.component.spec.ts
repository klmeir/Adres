import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdquisicionDetailComponent } from './adquisicion-detail.component';

describe('AdquisicionDetailComponent', () => {
  let component: AdquisicionDetailComponent;
  let fixture: ComponentFixture<AdquisicionDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdquisicionDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdquisicionDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
