import { Injectable } from '@angular/core';
import { Acquisition } from '../model/acquisition';
import { HttpService } from '@core/services/http.service';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdquisicionService {
  private api:string = 'http://localhost:5069/';
  private adquisicions: Acquisition[] = [];

  constructor(protected http: HttpService) {
    let savedAcquisition = localStorage.getItem('adquisicions');
    this.adquisicions = savedAcquisition
      ? JSON.parse(savedAcquisition)
      : [];
  }
  getAcquisitions(): Observable<Acquisition[]> {
    return this.http.doGet(`${this.api}acquisitions`)
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      .pipe(map((response: any) => response as Acquisition[]));
  }

  getAcquisition(id: string): Observable<Acquisition> {
    return this.http.doGet(`${this.api}acquisitions/${id}`)
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      .pipe(map((response: any) => response as Acquisition));
  }

  addAcquisition(acquisition: Acquisition) {
    return this.http.doPost<Acquisition, boolean>(`${this.api}acquisitions`, acquisition);
  }

  deleteAcquisition(id: string) {
    return this.http.doDelete<boolean>(`${this.api}acquisitions/${id}`);
  }

  updateAcquisition(id: string, acquisition: Acquisition) {
    return this.http.doPut<Acquisition, boolean>(`${this.api}acquisitions/${id}`, acquisition);
  }

  uploadFile(file: File) {
    return this.http.doPost<File, string>(`${this.api}files`, file);
  }
}
