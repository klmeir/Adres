import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdquisicionCreateComponent } from './adquisicion-create.component';

describe('AdquisicionCreateComponent', () => {
  let component: AdquisicionCreateComponent;
  let fixture: ComponentFixture<AdquisicionCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdquisicionCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdquisicionCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
