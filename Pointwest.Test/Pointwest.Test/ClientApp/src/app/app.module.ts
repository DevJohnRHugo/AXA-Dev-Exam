import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MatStepperModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatGridListModule, MatDatepickerModule, MatNativeDateModule } from '@angular/material';
import { MatFileUploadModule } from 'angular-material-fileupload';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { ParticlesModule } from 'angular-particle';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { ResumeUploaderComponent } from './resume-uploader/resume-uploader.component';
import { Home2Component } from './home2/home2.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { FooterComponent } from './footer/footer.component';
import { ApplicationProcessComponent } from './application-process/application-process.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegistrationFormComponent,
    ResumeUploaderComponent,
    Home2Component,
    ScheduleComponent,
    FooterComponent,
    ApplicationProcessComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    MatStepperModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule,
    MatFileUploadModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,
    NgxMatTimepickerModule,
    NgxMaterialTimepickerModule,
    MaterialFileInputModule,
    ParticlesModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: Home2Component, pathMatch: 'full' },
      //{ path: 'home', component: Home2Component },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'application/get-started', component: ApplicationProcessComponent },
      { path: 'applicant/register', component: RegistrationFormComponent },
      { path: 'applicant/file-upload', component: ResumeUploaderComponent },
      { path: 'applicant/schedule', component: ScheduleComponent },
    ]),
    BrowserAnimationsModule
  ],
  exports: [MatFormFieldModule, MatInputModule],
  providers: [ApplicationProcessComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
