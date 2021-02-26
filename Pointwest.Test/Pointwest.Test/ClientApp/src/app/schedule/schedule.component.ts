import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ApplicationService } from '../services/application.service';
//import { Component } from '@angular/core';
import { NgbDateStruct, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {
  today = new Date();
  time = { hour: this.today.getHours(), minute: this.today.getMinutes() };
  timeValue = "";
  meridian = true;
  model: NgbDateStruct;
  date: { year: number, month: number };
  dateValue = "";
  bookScheduleValues: any = null;

  constructor(private service: ApplicationService, private calendar: NgbCalendar, private toastrService: ToastrService) { }

  ngOnInit() {

  }

  selectToday() {
    this.dateValue = `${this.model.year}-${this.zeroPrefix(this.model.month)}-${this.zeroPrefix(this.model.day)}`
    console.log(this.dateValue);
  }

  setHourClockFormat() {
   
    if (this.time.hour >= 12 && this.time.minute >= 0) {
      this.timeValue = String(`${(this.time.hour > 12) ? this.time.hour - 12 : this.time.hour}${this.minuteFormatter(this.time.minute)}PM`);
    }
    else {

      this.timeValue = String(`${(this.time.hour == 0) ? 12 : this.time.hour}${this.minuteFormatter(this.time.minute)}AM`);
    }
    console.log(this.timeValue);
  }

  zeroPrefix(value: number) {
    if (value < 10) {
      return `0${value}`;
    }
    else {
      return value;
    }
  }

  minuteFormatter(minute: number) {
    return (minute < 1) ? "" : this.zeroPrefix(minute)
  }

  bookSchedule() {
    this.selectToday();
    this.setHourClockFormat();
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
}
