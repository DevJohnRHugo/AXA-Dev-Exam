import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ApplicationService } from '../services/application.service';
//import { Component } from '@angular/core';
import { NgbDateStruct, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

import * as moment from 'moment';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
//import { AppDateAdapter, APP_DATE_FORMATS } from '../custom-date-format/format-datepicker';
import { CustomNgxDatetimeAdapter, CUSTOM_DATE_FORMATS } from '../custom-date-format/format-datepicker';
import { NgxMatDateAdapter, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';


@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css'],
  providers: [
    //{ provide: DateAdapter, useClass: AppDateAdapter },
    //{ provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }
    { provide: NgxMatDateAdapter, useClass: CustomNgxDatetimeAdapter },
    { provide: NGX_MAT_DATE_FORMATS, useValue: CUSTOM_DATE_FORMATS }
  ]
})
export class ScheduleComponent implements OnInit {
  //today = new Date();
  //time = { hour: this.today.getHours(), minute: this.today.getMinutes() };
  timeValue = "";
  //meridian = true;
  //model: NgbDateStruct;
  //date: { year: number, month: number };
  dateValue = "";
  bookScheduleValues: any = null;

  events: string[] = [];

  constructor(private service: ApplicationService, private calendar: NgbCalendar, private toastrService: ToastrService) { }

  ngOnInit() {

  }

  //selectToday() {
  //  this.dateValue = `${this.model.year}-${this.zeroPrefix(this.model.month)}-${this.zeroPrefix(this.model.day)}`
  //  console.log(this.dateValue);
  //}

  //setHourClockFormat() {
   
  //  if (this.time.hour >= 12 && this.time.minute >= 0) {
  //    this.timeValue = String(`${(this.time.hour > 12) ? this.time.hour - 12 : this.time.hour}${this.minuteFormatter(this.time.minute)}PM`);
  //  }
  //  else {

  //    this.timeValue = String(`${(this.time.hour == 0) ? 12 : this.time.hour}${this.minuteFormatter(this.time.minute)}AM`);
  //  }
  //  console.log(this.timeValue);
  //}

  //zeroPrefix(value: number) {
  //  if (value < 10) {
  //    return `0${value}`;
  //  }
  //  else {
  //    return value;
  //  }
  //}

  //minuteFormatter(minute: number) {
  //  return (minute < 1) ? "" : this.zeroPrefix(minute)
  //}

  bookSchedule() {
    //this.selectToday();
    //this.setHourClockFormat();
    this.bookScheduleValues = {
      "ProposedDate": this.dateValue,
      "ProposedTime": this.timeValue
    }
    this.service.scheduleInterview(this.bookScheduleValues)
      .subscribe(
        response => {
          const responseMessage = JSON.parse(response.message);

          if (response.isSuccess) {
            this.toastrService.success(responseMessage.Message, "Success");
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
  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    //this.events.push(`${type}: ${event.value}`);
    this.dateValue = moment(event.value).format('YYYY-MM-DD');
    this.timeValue = moment(event.value).format('hmmA');
    console.log(this.timeValue);
  }
}
