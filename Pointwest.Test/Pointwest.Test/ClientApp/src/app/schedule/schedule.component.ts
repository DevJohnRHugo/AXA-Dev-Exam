import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../services/application.service';
import { NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

import * as moment from 'moment';
import { CustomNgxDatetimeAdapter, CUSTOM_DATE_FORMATS } from '../custom-date-format/format-datepicker';
import { NgxMatDateAdapter, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { ApplicationProcessComponent } from '../application-process/application-process.component';


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
  timeValue = "";
  dateValue = "";
  bookScheduleValues: any = null;
  events: string[] = [];
  isScheduleSuccess: boolean;

  constructor(private service: ApplicationService, private applicationProcess: ApplicationProcessComponent, private calendar: NgbCalendar, private toastrService: ToastrService) { }

  ngOnInit() {

  }

  bookSchedule() {    
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
            this.applicationProcess.isScheduleSuccess = true;
            this.isScheduleSuccess = true;
          }
          else {
            this.toastrService.error(responseMessage.Message, "Error");
            this.applicationProcess.isScheduleSuccess = true;
            this.isScheduleSuccess = true;
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
  }
}
