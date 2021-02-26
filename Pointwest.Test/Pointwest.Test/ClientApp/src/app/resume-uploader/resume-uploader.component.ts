import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RegistrationFormComponent } from '../registration-form/registration-form.component';
import { ApplicationService } from '../services/application.service';

@Component({
  selector: 'app-resume-uploader',
  templateUrl: './resume-uploader.component.html',
  styleUrls: ['./resume-uploader.component.css']
})
export class ResumeUploaderComponent implements OnInit {
  selectedFile = null;

  constructor(private service: ApplicationService, private route: Router, private toastrService: ToastrService) { }

  ngOnInit() {
  }

  onFileSelected(event) {
    this.selectedFile = event.target.files[0];
  }

  uploadResume() { 
    
    this.service.uploadFile(this.selectedFile)
      .subscribe(
        response => {
          const responseMessage = JSON.parse(response.message)

          if (response.isSuccess) {
            this.toastrService.success(responseMessage.Message, "Success");
            this.route.navigate(['/applicant/schedule']);
          }
          else {
            this.toastrService.error(responseMessage.Message, "Error");
          }
          
        }, error => {
          const parseJson = JSON.stringify(error.error)
          const errorResponse = JSON.parse(parseJson);

          this.toastrService.error(errorResponse.message, "Error");
        });
  }

}
