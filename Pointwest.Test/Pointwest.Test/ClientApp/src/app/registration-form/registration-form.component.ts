import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApplicationService } from '../services/application.service';

@Component({
  selector: 'registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {
  form = new FormGroup({
    name: new FormControl(),
    email: new FormControl(),
    mobile: new FormControl(),
    positionApplied: new FormControl(),
    source: new FormControl(),
    //Id: new FormControl()
  });

  constructor(private service: ApplicationService, private route: Router, private toastrService: ToastrService) { }

  ngOnInit() {
  }

  registerUser() {
    this.service.registerUser(this.form.value)
      .subscribe(
        response => {         
          const responseMessage = JSON.parse(response.message);

          if (response.isSuccess) {
            this.toastrService.success(responseMessage.Message, "Success");
            this.route.navigate(['/applicant/file-upload']);
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
