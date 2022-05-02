import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { MisDashboardComponent } from "./layouts/dashboard/mis-dashboard.component";
import { LoginComponent } from "./views/auth/login/login.component";
import { AuthComponent } from "./layouts/auth/auth.component";
import { DiseaseComponent } from "./views/disease/disease.component";
import { DashboardComponent } from "./views/dashboard/dashboard.component";
import { AuthGuard } from "./_helpers/auth.guard";
import { AnimalsComponent } from "./views/animals/animals.component";
import { DistrictsComponent } from "./views/districts/districts.component";
import { InstitutionsComponent } from "./views/institutions/institutions.component";
import { InChargeComponent } from "./views/in-charge/in-charge.component";
import { UserManagementComponent } from "./views/user-management/user-management.component";
import { LaboratoryComponent } from "./views/laboratory/laboratory.component";
import { SamplesComponent } from "./views/samples/samples.component";
import { LaboratoryExaminationComponent } from "./views/forms/laboratory-examination/laboratory-examination.component";
import { EntryFormComponent } from "./views/forms/entry-form/entry-form.component";
import {ReportsComponent} from "./views/reports/reports.component";
import {NonInfectiousReportsComponent} from "./views/reports/non-infectious-reports/non-infectious-reports.component";

const routes: Routes = [
  {
    path: "",
    component: AuthComponent,
    children: [
      { path: "login", component: LoginComponent },
      { path: "", redirectTo: "login", pathMatch: "full" },
    ],
  },
  {
    path: "mis",
    component: MisDashboardComponent,
    canActivate: [AuthGuard],
    children: [
      { path: "dashboard", component: DashboardComponent },
      { path: "user-management", component: UserManagementComponent },
      { path: "districts", component: DistrictsComponent },
      { path: "institutes", component: InstitutionsComponent },
      { path: "in-charge", component: InChargeComponent },
      { path: "laboratories", component: LaboratoryComponent },
      { path: "diseases", component: DiseaseComponent },
      { path: "animals", component: AnimalsComponent },
      { path: "samples", component: SamplesComponent },
      { path: "forms/:formName", component: EntryFormComponent },
      { path: "laboratory", component: LaboratoryExaminationComponent },
      {
        path: "reports", component: ReportsComponent,
        children: [
          { path: "non-infectious", component: NonInfectiousReportsComponent },

        ]
      },
      { path: "", redirectTo: "dashboard", pathMatch: "full" },
    ]
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload', useHash: true })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
