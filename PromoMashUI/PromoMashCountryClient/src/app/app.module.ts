import {NgModule} from '@angular/core';
import {BrowserModule, Title} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import { RouterModule } from '@angular/router';
import {AuthConfig, OAuthModule, OAuthStorage} from "angular-oauth2-oidc";
import {HttpClientModule} from "@angular/common/http";
import {authCodeFlowConfig} from "./auth/auth.config";
import {AuthLocalStorageService} from "./auth/auth.localStorage.service";
import { PageComponent } from './page/page.component';
import {LoginCompletedComponent} from "./pages/login-completed/login-completed.component";
import {LogoutCompletedComponent} from "./pages/logout-completed/logout-completed.component";
import {HomeComponent} from "./pages/protected/home/home.component";
import {LoginComponent} from "./pages/login/login.component";
import {UserInfoComponent} from "./pages/protected/user-info/user-info.component";
import {ApiCallComponent} from "./pages/protected/api-call/api-call.component";
import {CountryComponent} from "./pages/protected/country/country.component";
import { LogoutComponent } from './pages/logout/logout.component';
import { MainComponent } from './pages/main/main.component';
import { RouterOutlet } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    LoginCompletedComponent,
    LogoutCompletedComponent,
    ApiCallComponent,
    HomeComponent,
    LoginComponent,
    UserInfoComponent,
    PageComponent,
    LogoutComponent,
    MainComponent,
    CountryComponent
  ],
  imports: [
    RouterOutlet,
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    NgSelectModule,
    FormsModule,
    HttpClientModule,
    OAuthModule.forRoot()
  ],
  providers: [
    Title,
    {
      provide: AuthConfig,
      useValue: authCodeFlowConfig
    },
    {
      provide: OAuthStorage,
      useClass: AuthLocalStorageService,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
