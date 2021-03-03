import { Component, EventEmitter, OnInit } from '@angular/core';
import { ApplicationService } from '../services/application.service';
import { NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

import * as moment from 'moment';
import { CustomNgxDatetimeAdapter, CUSTOM_DATE_FORMATS } from '../custom-date-format/format-datepicker';
import { NgxMatDateAdapter, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { ApplicationProcessComponent } from '../application-process/application-process.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css'],
  providers: [
    { provide: NgxMatDateAdapter, useClass: CustomNgxDatetimeAdapter },
    { provide: NGX_MAT_DATE_FORMATS, useValue: CUSTOM_DATE_FORMATS }
  ]
})
export class ScheduleComponent implements OnInit {
  scheduleFormGroup: FormGroup;
  timeValue = "";
  dateValue = "";
  bookScheduleValues: any = null;
  events: string[] = [];
  isScheduleSuccess: boolean;
  isShowSpinner: boolean;
  isToAutomate = false;

  constructor(private service: ApplicationService, private applicationProcess: ApplicationProcessComponent, private calendar: NgbCalendar, private toastrService: ToastrService, private formBuilder: FormBuilder) { }

  ngOnInit() {
     this.scheduleFormGroup = this.formBuilder.group({
       schedule: ['', Validators.required]
    });
  }

  bookSchedule() {
    this.isShowSpinner = true;
    this.bookScheduleValues = {
      "ProposedDate": this.dateValue,
      "ProposedTime": this.timeValue
    }

    this.service.scheduleInterview(this.bookScheduleValues,  this.isToAutomate)
      .subscribe(
        response => {
          const responseMessage = JSON.parse(response.message);

          if (response.isSuccess) {
            this.isShowSpinner = false;

            this.toastrService.success(responseMessage.Message, "Success");

            this.applicationProcess.isScheduleSuccess = true;
            this.isScheduleSuccess = true;
          }
          else {
            this.isShowSpinner = false;

            this.toastrService.info(responseMessage.Message, "Info");

            this.applicationProcess.isScheduleSuccess = true;
            this.isScheduleSuccess = true;
          }

        }, error => {
          const parseJson = JSON.stringify(error.error)
          const errorResponse = JSON.parse(parseJson);
          this.isShowSpinner = false;

          console.log(errorResponse);
          this.toastrService.error("Please check input values", "Error");
        });
  }
  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this.dateValue = moment(event.value).format('YYYY-MM-DD');
    this.timeValue = moment(event.value).format('hmmA');
  }

  slideToggled(automate) {
    this.isToAutomate = automate._checked;
    console.log(this.isToAutomate);
  }
}
