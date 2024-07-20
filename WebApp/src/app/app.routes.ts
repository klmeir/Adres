import { Routes } from '@angular/router';
import { AdquisicionCreateComponent } from '@adquisicion/components/adquisicion-create/adquisicion-create.component';
import { AdquisicionDetailComponent } from '@adquisicion/components/adquisicion-detail/adquisicion-detail.component';
import { AdquisicionListComponent } from '@adquisicion/components/adquisicion-list/adquisicion-list.component';
import { NavbarComponent } from '@core/components/navbar/navbar.component';

export const routes: Routes = [
  { path: '', component: NavbarComponent },
  { path: 'list', component: AdquisicionListComponent },
  { path: 'new', component: AdquisicionCreateComponent },
  { path: 'edit/:id', component: AdquisicionCreateComponent },
];
