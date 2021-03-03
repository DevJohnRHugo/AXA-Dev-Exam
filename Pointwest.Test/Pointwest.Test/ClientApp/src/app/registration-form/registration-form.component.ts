import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ApplicationService } from '../services/application.service';
import { ApplicationProcessComponent } from '../application-process/application-process.component';

@Component({
  selector: 'registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {
  registrationFormGroup: FormGroup;
  isRegistrationSuccess: boolean;
  isShowSpinner: boolean;

  constructor(private service: ApplicationService, private applicationProcess: ApplicationProcessComponent, private toastrService: ToastrService, private formBuilder: FormBuilder) {

  }

  ngOnInit() {
    this.registrationFormGroup = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      mobile: ['', Validators.required],
      positionApplied: ['', Validators.required],
      source: ['', Validators.required]
    });
  }

  registerUser() {
    let mobile = this.registrationFormGroup.controls["mobile"].value as number;

    this.registrationFormGroup.controls["mobile"].setValue(`0${mobile.toString()}`);
    this.isShowSpinner = true;

    this.service.registerUser(this.registrationFormGroup.value)
      .subscribe(
        response => {
          const responseMessage = JSON.parse(response.message);

          if (response.isSuccess) {
            this.isShowSpinner = false;

            this.toastrService.success(responseMessage.Message, "Success");

            this.applicationProcess.isRegistrationSuccess = true;
            this.isRegistrationSuccess = true;
          }
          else {
            this.isShowSpinner = false;

            this.toastrService.info(responseMessage.Message, "Info");

            this.applicationProcess.isRegistrationSuccess = true;
            this.isRegistrationSuccess = true;
          }
        }, error => {
          const parseJson = JSON.stringify(error.error)
          const errorResponse = JSON.parse(parseJson);
          this.isShowSpinner = false;
          console.log(errorResponse);
          this.toastrService.error(errorResponse.message, "Error");
        });
  }
}
