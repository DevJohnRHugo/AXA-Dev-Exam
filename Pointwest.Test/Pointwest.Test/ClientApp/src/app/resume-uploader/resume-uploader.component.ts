import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ApplicationProcessComponent } from '../application-process/application-process.component';
import { ApplicationService } from '../services/application.service';

@Component({
  selector: 'app-resume-uploader',
  templateUrl: './resume-uploader.component.html',
  styleUrls: ['./resume-uploader.component.css']
})
export class ResumeUploaderComponent implements OnInit {
  uploadResumeFormGroup: FormGroup;
  selectedFile = null;
  isUploadingSuccess: boolean;
  isShowSpinner: boolean;

  constructor(private service: ApplicationService, private applicationProcess: ApplicationProcessComponent, private toastrService: ToastrService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.uploadResumeFormGroup = this.formBuilder.group({
      resume: ['', Validators.required]
    });
  }

  onFileSelected(event) {
    this.selectedFile = event.target.files[0];
  }

  uploadResume() {
    this.isShowSpinner = true;

    this.service.uploadFile(this.selectedFile)
      .subscribe(
        response => {
          const responseMessage = JSON.parse(response.message)

          if (response.isSuccess) {
            this.isShowSpinner = false;

            this.toastrService.success(responseMessage.Message, "Success");

            this.applicationProcess.isUploadingSuccess = true;
            this.isUploadingSuccess = true;
          }
          else {
            this.isShowSpinner = false;

            this.toastrService.info(responseMessage.Message, "Info");

            this.applicationProcess.isUploadingSuccess = true;
            this.isUploadingSuccess = true;
          }

        }, error => {
          const parseJson = JSON.stringify(error.error)
          const errorResponse = JSON.parse(parseJson);
          this.isShowSpinner = false;

          this.toastrService.error(errorResponse.message, "Error");
        });
  }

}
