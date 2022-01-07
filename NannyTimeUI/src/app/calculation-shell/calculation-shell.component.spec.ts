import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalculationShellComponent } from './calculation-shell.component';

describe('CalculationShellComponent', () => {
  let component: CalculationShellComponent;
  let fixture: ComponentFixture<CalculationShellComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CalculationShellComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CalculationShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
