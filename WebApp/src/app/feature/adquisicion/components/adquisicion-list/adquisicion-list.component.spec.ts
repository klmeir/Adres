import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdquisicionListComponent } from './adquisicion-list.component';

describe('AdquisicionListComponent', () => {
  let component: AdquisicionListComponent;
  let fixture: ComponentFixture<AdquisicionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdquisicionListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdquisicionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
