import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApplicationService } from '../services/application.service';

import * as moment from 'moment';

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
  //uploadResumeFormGroup: FormGroup;
  //scheduleFormGroup: FormGroup;

  //selectedFile = null;

  //date: Date = new Date("Sat Feb 27 2021 13:42:19 GMT+0800 (China Standard Time)");

  //events: string[] = [];

  constructor(private service: ApplicationService, private toastrService: ToastrService, private _formBuilder: FormBuilder)
  {
    
  }

  ngOnInit() {
    this.registrationFormGroup = this._formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required, Validators.email,],
      mobile: ['', Validators.required],
      positionApplied: ['', Validators.required],
      source: ['', Validators.required]
    });
    //this.uploadResumeFormGroup = this._formBuilder.group({
    //  secondCtrl: ['', Validators.required]
    //});
    //this.scheduleFormGroup = this._formBuilder.group({
    //  schedule: [new Date(), Validators.required]
    //});
  }  

  registerUser() {
    console.log('call');
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

  //uploadResume() {
  //  this.service.uploadFile(this.selectedFile)
  //    .subscribe(
  //      response => {
  //        const responseMessage = JSON.parse(response.message)

  //        if (response.isSuccess) {
  //          this.toastrService.success(responseMessage.Message, "Success");
  //          //this.route.navigate(['/applicant/schedule']);
  //        }
  //        else {
  //          this.toastrService.error(responseMessage.Message, "Error");
  //        }

  //      }, error => {
  //        const parseJson = JSON.stringify(error.error)
  //        const errorResponse = JSON.parse(parseJson);

  //        this.toastrService.error(errorResponse.message, "Error");
  //      });
  //}

  //addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
  //  this.events.push(`${type}: ${event.value}`);
  //  let today = new Date(event.value).getHours();
  //  //this.scheduleFormGroup.controls['schedule'].setValue(moment(event.value).format('l, H:mm:ss'));
  //  console.log(moment(event.value).format('l, H:mm:ss'));
  //}

}
