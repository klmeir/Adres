import { Acquisition } from '@adquisicion/shared/model/acquisition';
import { AdquisicionService } from '@adquisicion/shared/service/adquisicion.service';
import { Component, OnInit, inject  } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from "../../../../core/components/navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { FakeImageUploadService } from '@adquisicion/shared/service/fake-image-upload.service';
import { DocumentationFile } from '@adquisicion/shared/model/documentation-file';

@Component({
  selector: 'app-adquisicion-create',
  standalone: true,
  imports: [NavbarComponent, CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './adquisicion-create.component.html',
  styleUrl: './adquisicion-create.component.css'
})
export class AdquisicionCreateComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  fakeImageUploadService = inject(FakeImageUploadService);
  edit = false;
  uploading = false;
  selectedImages!: FileList;
  documentationFiles: DocumentationFile[] | undefined = [];

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
      this.documentationFiles?.forEach(element => {
        if(!registration.documentation?.some(x => x.id === element.id))
          registration.documentation?.push(element);
      });

      let id = this.activatedRoute.snapshot.paramMap.get('id');

      if (id) {
        this.registrationService.updateAcquisition(id, registration)
        .subscribe(() => window.alert("Update Acquisition"));
      } else {
        this.registrationService.addAcquisition(registration)
        .subscribe(() => window.alert("Create Acquisition"));
      }

      this.router.navigate(['/list']);
    }
  }
  onImageSelected(event: Event): void {
    const inputElement = event.target as HTMLInputElement;

    if (inputElement?.files && inputElement.files.length > 0) {
      this.selectedImages = inputElement.files;
    }
  }
  upload(): void {
    this.uploading = true;
    if (this.selectedImages) {
      this.uploadFiles(this.selectedImages);
    }
  }
  private uploadFiles(images: FileList): void {
    console.log("call uploadFiles");
    this.uploading = true;

    for (let index = 0; index < images.length; index++) {
      const element = images[index];
      this.fakeImageUploadService.uploadImage(element)
      .subscribe((p) => {
        this.documentationFiles?.push({ name : element.name, url: p } as DocumentationFile);
        this.uploading = false;
      });
    }
  }
}
