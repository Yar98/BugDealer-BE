import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthencationRoutingModule } from './authencation-routing.module';
import { LoginComponent } from './pages/login/login.component';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';


@NgModule({
  declarations: [
    LoginComponent,
    ResetPasswordComponent
  ],
  imports: [
    CommonModule,
    AuthencationRoutingModule
  ]
})
export class AuthencationModule { }
