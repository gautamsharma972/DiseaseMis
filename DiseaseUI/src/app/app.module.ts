import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { JwtInterceptor } from "./_helpers/jwt.interceptor";
import { ErrorInterceptor } from "./_helpers/error.interceptor";

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { MisDashboardComponent } from './layouts/dashboard/mis-dashboard.component';
import { AppRoutingModule } from "./app-routing.module";
import { LoginComponent } from './views/auth/login/login.component';
import { AuthComponent } from './layouts/auth/auth.component';
import { DiseaseComponent } from './views/disease/disease.component';
import { UserManagementComponent } from './views/user-management/user-management.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { EmailValidatorDirective } from "./utils/email-validator.directive";
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { AuthenticationService } from "./_services/authentication.service";
import { User } from "./_models/user";
import { CommonModule, DatePipe } from "@angular/common";
import { AnimalsComponent } from './views/animals/animals.component';
import { InstitutionsComponent } from './views/institutions/institutions.component';
import { DistrictsComponent } from './views/districts/districts.component';
import { InChargeComponent } from './views/in-charge/in-charge.component';
import { FooterComponent } from './components/footer/footer.component';
import { UserDropdownComponent } from './components/dropdowns/user-dropdown/user-dropdown.component';
import { HotToastModule } from '@ngneat/hot-toast';
import { ApiService } from "./_services/api.service";
import { EditDistrictsComponent } from './views/districts/components/edit-districts.component';
import { EditInstitutesComponent } from './views/institutions/components/edit-institutes.component';
import { Ng2SearchPipeModule } from "ng2-search-filter";
import { SweetAlert2Module } from "@sweetalert2/ngx-sweetalert2";
import { EditInchargesComponent } from "./views/in-charge/components/edit-incharges.component";
import { EditDiseaseComponent } from "./views/disease/components/edit-disease.component";
import { EditAnimalComponent } from "./views/animals/components/edit-animal.component";
import { LaboratoryComponent } from './views/laboratory/laboratory.component';
import { EditLaboratoriesComponent } from "./views/laboratory/components/edit-laboratories.component";
import { CategoryTableComponent } from './views/animals/components/category-table/category-table.component';
import { AnimalTableComponent } from './views/animals/components/animal-table/animal-table.component';
import { SamplesComponent } from './views/samples/samples.component';
import { EditSampleComponent } from "./views/samples/components/edit-sample.component";
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component'; 
import { EditUserComponent } from './views/user-management/components/edit-user.component';
import { DiseaseFormComponent } from './views/forms/disease-form/disease-form.component';
import { EntryFormComponent } from './views/forms/entry-form/entry-form.component';
import { LaboratoryExaminationComponent } from './views/forms/laboratory-examination/laboratory-examination.component';
import { LaboratoryFormComponent } from './views/forms/laboratory-form/laboratory-form.component';
import { CalcDirective } from './utils/calc-directive';
import { DisableDirective } from './utils/disable-directive';
import { ReportsComponent } from './views/reports/reports.component';
import { NonInfectiousReportsComponent } from './views/reports/non-infectious-reports/non-infectious-reports.component';
import { InfectiousReportsComponent } from './views/reports/infectious-reports/infectious-reports.component';
import { LaboratoryReportsComponent } from './views/reports/laboratory-reports/laboratory-reports.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { SharedService } from './_services/shared.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NavbarComponent,
    SidebarComponent,
    MisDashboardComponent,
    LoginComponent,
    AuthComponent,
    DiseaseComponent,
    UserManagementComponent,
    EmailValidatorDirective,
    DashboardComponent,
    AnimalsComponent,
    InstitutionsComponent,
    DistrictsComponent,
    InChargeComponent,
    FooterComponent,
    UserDropdownComponent,
    EditInstitutesComponent,
    EditDistrictsComponent,
    EditInchargesComponent,
    EditDiseaseComponent,
    EditAnimalComponent,
    LaboratoryComponent,
    EditLaboratoriesComponent,
    CategoryTableComponent,
    AnimalTableComponent,
    SamplesComponent,
    EditSampleComponent,
    BreadcrumbComponent, 
    EditUserComponent,
    DiseaseFormComponent,
    EntryFormComponent,
    DiseaseFormComponent,
    LaboratoryExaminationComponent,
    LaboratoryFormComponent,
    CalcDirective,
    DisableDirective,
    ReportsComponent,
    NonInfectiousReportsComponent,
    InfectiousReportsComponent,
    LaboratoryReportsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    HotToastModule.forRoot(),
    Ng2SearchPipeModule,
    SweetAlert2Module.forRoot(),
    DragDropModule
  ],
  providers: [
    AuthenticationService,
    ApiService,
    SharedService,
    User,
    DatePipe,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
