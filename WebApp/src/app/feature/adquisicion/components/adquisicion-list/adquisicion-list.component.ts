import { Component } from '@angular/core';
import { NavbarComponent } from "../../../../core/components/navbar/navbar.component";
import { AdquisicionService } from '@adquisicion/shared/service/adquisicion.service';
import { Acquisition } from '@adquisicion/shared/model/acquisition';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AcquisitionFilter } from '@adquisicion/shared/model/acquisition-filter';

@Component({
  selector: 'app-adquisicion-list',
  standalone: true,
  imports: [NavbarComponent, RouterModule, CommonModule,ReactiveFormsModule],
  templateUrl: './adquisicion-list.component.html',
  styleUrl: './adquisicion-list.component.css'
})
export class AdquisicionListComponent {
  firstDayOfMonth:Date;
  lastDayOfMonth:Date;
  filtersForm: FormGroup;
  public adquisicions: Observable<Acquisition[]>;

  constructor(
    private formBuilder: FormBuilder,
    private adquisicionService: AdquisicionService,
    private router: Router) { }

  ngOnInit(): void {
    this.firstDayOfMonth = new Date();
    this.firstDayOfMonth.setDate(1);

    this.lastDayOfMonth = new Date();
    this.lastDayOfMonth.setMonth(this.lastDayOfMonth.getMonth() + 4);
    this.lastDayOfMonth.setDate(0);

    this.filtersForm = this.formBuilder.group({
      startDate: [this.firstDayOfMonth.toISOString().substring(0, 10), Validators.required],
      endDate: [this.lastDayOfMonth.toISOString().substring(0, 10), Validators.required],
      search: ['']
    });
    const acquisitionFilter: AcquisitionFilter = this.filtersForm.value;
    this.adquisicions = this.adquisicionService.getAcquisitions(acquisitionFilter);
  }

  search():void {
    console.log("call search");
    if (this.filtersForm.valid) {
      const acquisitionFilter: AcquisitionFilter = this.filtersForm.value;

      this.adquisicions = this.adquisicionService.getAcquisitions(acquisitionFilter);
    }
  }

  deleteAcquisition(id: number) {
    this.adquisicionService.deleteAcquisition(id)
      .subscribe(() => {
        this.search();
      });;
  }
}
