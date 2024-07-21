import { Component } from '@angular/core';
import { NavbarComponent } from "../../../../core/components/navbar/navbar.component";
import { AdquisicionService } from '@adquisicion/shared/service/adquisicion.service';
import { Acquisition } from '@adquisicion/shared/model/acquisition';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-adquisicion-list',
  standalone: true,
  imports: [NavbarComponent, RouterModule, CommonModule],
  templateUrl: './adquisicion-list.component.html',
  styleUrl: './adquisicion-list.component.css'
})
export class AdquisicionListComponent {
  public adquisicions: Observable<Acquisition[]>;

  constructor(private adquisicionService: AdquisicionService) {}

  ngOnInit(): void {
    this.adquisicions = this.adquisicionService.getAcquisitions();
  }

  deleteAcquisition(id: number) {
    this.adquisicionService.deleteAcquisition(id)
    .subscribe(() => window.alert("Delete Acquisition"));;
  }
}
