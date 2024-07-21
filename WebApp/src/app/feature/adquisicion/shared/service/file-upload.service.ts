import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, delay, of, timeout } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FileUploadResponseDto } from '../model/file-upload-response';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  constructor(
    private http: HttpClient
  ) { }

  uploadFile(file: File): Observable<FileUploadResponseDto> {
    let formData: FormData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<FileUploadResponseDto>(
      `${environment.endpoint}/files`,
      formData,
      { headers: this.getUploadOptions() }
    )
  }

  private getUploadOptions = (): HttpHeaders => {
    const headers = new HttpHeaders();
    headers.set('Accept', 'text/plain');
    headers.set('Content-Type', 'multipart/form-data');
    return headers;
  }
}
