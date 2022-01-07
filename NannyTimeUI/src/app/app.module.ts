import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input'
import { AppRoutingModule } from './app-routing.module';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { UiShellComponent } from './ui-shell/ui-shell.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { CalculationShellComponent } from './calculation-shell/calculation-shell.component';

const appRoutes: Routes = [
  // { path: 'company', component: CompanyComponent },
  // { path: 'employee/:id',      component: EmployeeDetailComponent },
  // {
  //   path: 'employees',
  //   component: EmployeeListComponent,
  //   data: { title: 'Employees List' }
  // },
  { path: '',
    redirectTo: '/clock',
    pathMatch: 'full'
  },
  // { path: '**', component: PageNotFoundComponent }
  {path:'calculations',
component:CalculationShellComponent},

{path: 'clock',
component: UiShellComponent}


];

@NgModule({
  declarations: [
    AppComponent,
    UiShellComponent,
    CalculationShellComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes,{enableTracing: false}),
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatButtonModule,
    CommonModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
