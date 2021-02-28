import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-application-process',
  templateUrl: './application-process.component.html',
  styleUrls: ['./application-process.component.css']
})
export class ApplicationProcessComponent implements OnInit {
  isRegistrationSuccess: boolean;
  isUploadingSuccess: boolean;
  isScheduleSuccess: boolean;

  constructor() { }

  ngOnInit() {
  }

}
