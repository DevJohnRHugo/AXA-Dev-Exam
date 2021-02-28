import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ApplicationProcessComponent } from '../application-process/application-process.component';
import { ApplicationService } from '../services/application.service';

@Component({
  selector: 'app-resume-uploader',
  templateUrl: './resume-uploader.component.html',
  styleUrls: ['./resume-uploader.component.css']
})
export class ResumeUploaderComponent implements OnInit {
  selectedFile = null;
  isUploadingSuccess: boolean;

  constructor(private service: ApplicationService, private applicationProcess: ApplicationProcessComponent, private toastrService: ToastrService) { }

  ngOnInit() {
  }

  onFileSelected(event) {
    this.selectedFile = event.target.files[0];
    console.log(this.selectedFile);
  }

  uploadResume() {     
    this.service.uploadFile(this.selectedFile)
      .subscribe(
        response => {
          const responseMessage = JSON.parse(response.message)

          if (response.isSuccess) {
            this.toastrService.success(responseMessage.Message, "Success");
            this.applicationProcess.isUploadingSuccess = true;
            this.isUploadingSuccess = true;
          }
          else {
            this.toastrService.error(responseMessage.Message, "Error");
            this.applicationProcess.isUploadingSuccess = true;
            this.isUploadingSuccess = true;
          }
          
        }, error => {
          const parseJson = JSON.stringify(error.error)
          const errorResponse = JSON.parse(parseJson);

          this.toastrService.error(errorResponse.message, "Error");
        });
  }

}
