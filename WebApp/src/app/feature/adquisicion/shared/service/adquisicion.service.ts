import { Injectable } from '@angular/core';
import { Acquisition } from '../model/acquisition';
import { HttpService } from '@core/services/http.service';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FileUploadService } from './file-upload.service';
import { FileUploadResponseDto } from '../model/file-upload-response';

@Injectable({
  providedIn: 'root'
})
export class AdquisicionService {

  constructor(protected http: HttpService, protected fileUpload: FileUploadService) {
  }
  getAcquisitions(): Observable<Acquisition[]> {
    return this.http.doGet(`${environment.endpoint}/acquisitions`)
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      .pipe(map((response: any) => response as Acquisition[]));
  }

  getAcquisition(id: string): Observable<Acquisition> {
    return this.http.doGet(`${environment.endpoint}/acquisitions/${id}`)
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      .pipe(map((response: any) => response as Acquisition));
  }

  addAcquisition(acquisition: Acquisition) {
    return this.http.doPost<Acquisition, boolean>(`${environment.endpoint}/acquisitions`, acquisition);
  }

  deleteAcquisition(id: number) {
    return this.http.doDelete<boolean>(`${environment.endpoint}/acquisitions/${id}`);
  }

  updateAcquisition(id: string, acquisition: Acquisition) {
    return this.http.doPut<Acquisition, boolean>(`${environment.endpoint}/acquisitions/${id}`, acquisition);
  }

  uploadFile(file: File): Observable<FileUploadResponseDto> {
    return this.fileUpload.uploadFile(file);
  }
}
