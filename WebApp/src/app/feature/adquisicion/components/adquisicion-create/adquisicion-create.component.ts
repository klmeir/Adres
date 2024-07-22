import { Acquisition } from '@adquisicion/shared/model/acquisition';
import { AdquisicionService } from '@adquisicion/shared/service/adquisicion.service';
import { Component, OnInit, inject  } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NavbarComponent } from "../../../../core/components/navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { DocumentationFile } from '@adquisicion/shared/model/documentation-file';

@Component({
  selector: 'app-adquisicion-create',
  standalone: true,
  imports: [NavbarComponent, CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './adquisicion-create.component.html',
  styleUrl: './adquisicion-create.component.css'
})
export class AdquisicionCreateComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  edit = false;
  uploading = false;
  selectedImages!: FileList;
  documentationFiles: DocumentationFile[];

  constructor(
    private formBuilder: FormBuilder,
    private registrationService: AdquisicionService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      id: [0, Validators.required],
      budget: ['', Validators.required],
      unit: ['', Validators.required],
      type: ['', Validators.required],
      quantity: ['', Validators.required],
      unitValue: ['', Validators.required],
      totalValue: ['', Validators.required],
      acquisitionDate: ['', Validators.required],
      supplier: ['', Validators.required],
    });
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id) {
      this.registrationService.getAcquisition(id).subscribe((registration) => {
        if (registration){
          this.registerForm.patchValue(registration);
          this.documentationFiles = registration.documentation;
          this.edit = true;
        }
      });
    }
  }
  onSubmit() {
    if (this.registerForm.valid) {
      let registration: Acquisition = this.registerForm.value;
      let id = this.activatedRoute.snapshot.paramMap.get('id');

      registration.documentation = this.documentationFiles;

      if (id) {
        console.log("call update", JSON.stringify(registration));
        this.registrationService.updateAcquisition(id, registration)
        .subscribe(() => {
          //window.alert("Update Acquisition");
          this.router.navigate(['/list']);
        });
      } else {
        registration.documentation = this.documentationFiles;
        this.registrationService.addAcquisition(registration)
        .subscribe(() => {
          //window.alert("Create Acquisition");
          this.router.navigate(['/list']);
        });
      }
    }
  }
  onImageSelected(event: Event): void {
    const inputElement = event.target as HTMLInputElement;

    if (inputElement?.files && inputElement.files.length > 0) {
      this.selectedImages = inputElement.files;
    }
  }
  upload(): void {
    if (this.selectedImages) {
      this.uploadFiles(this.selectedImages);
    }
  }
  private uploadFiles(images: FileList): void {
    this.uploading = true;

    for (let index = 0; index < images.length; index++) {
      const element = images[index];
      this.registrationService.uploadFile(element)
      .subscribe((p) => {
        const documentationFile = { id: 0, name : element.name, url: p.url } as DocumentationFile;
        console.log(JSON.stringify(documentationFile));
        this.documentationFiles.push(documentationFile);
        this.uploading = false;
      });
    }
  }
}
