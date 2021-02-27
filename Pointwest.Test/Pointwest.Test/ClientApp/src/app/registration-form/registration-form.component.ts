import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApplicationService } from '../services/application.service';

@Component({
  selector: 'registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {
  //form = new FormGroup({
  //  name: new FormControl(),
  //  email: new FormControl(),
  //  mobile: new FormControl(),
  //  positionApplied: new FormControl(),
  //  source: new FormControl(),
  //  //Id: new FormControl()
  //});

  isLinear = false;
  registrationFormGroup: FormGroup;
  uploadResumeFormGroup: FormGroup;
  scheduleFormGroup: FormGroup;

  constructor(private service: ApplicationService,/* private route: Router,*/ private toastrService: ToastrService,private _formBuilder: FormBuilder) { }

  ngOnInit() {
    this.registrationFormGroup = this._formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required, Validators.email,],
      mobile: ['', Validators.required],
      positionApplied: ['', Validators.required],
      source: ['', Validators.required]
    });
    this.uploadResumeFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
    this.scheduleFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }

  registerUser() {
    let mobile = this.registrationFormGroup.controls["mobile"].value as number;
    this.registrationFormGroup.controls["mobile"].setValue(`0${mobile.toString()}`);
    this.service.registerUser(this.registrationFormGroup.value)
      .subscribe(
        response => {         
          const responseMessage = JSON.parse(response.message);

          if (response.isSuccess) {
            this.toastrService.success(responseMessage.Message, "Success");
            //this.route.navigate(['/applicant/file-upload']);
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
