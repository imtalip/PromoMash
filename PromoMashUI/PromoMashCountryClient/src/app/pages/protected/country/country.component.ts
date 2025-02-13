import { Component, OnInit } from '@angular/core';
import {OAuthService} from "angular-oauth2-oidc";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {countryResponseDto} from './../../../interfaces/countryResponseDto';
import {Observable, of } from 'rxjs'
import { FormControl } from '@angular/forms';
import { NgLabelTemplateDirective, NgOptionTemplateDirective, NgSelectComponent, NgSelectConfig } from '@ng-select/ng-select';

@Component({
    selector: 'ng-country',
    templateUrl: './country.component.html',
    styleUrls: ['./country.component.scss'],
    standalone: false
})

export class CountryComponent implements OnInit {
	people$: Observable<Array<countryResponseDto>>;
	selectedPersonId = '1';
  accessToken = '';

 constructor(private oauthService: OAuthService, private httpClient: HttpClient, private config: NgSelectConfig) {
      /*this.config.notFoundText = 'Custom not found';
      this.config.appendTo = 'body';
      // set the bindValue to global config when you use the same 
      // bindValue in most of the place. 
      // You can also override bindValue for the specified template 
      // by defining `bindValue` as property
      // Eg : <ng-select bindValue="some-new-value"></ng-select>
      this.config.bindValue = 'value';*/
  }

	ngOnInit() {
    this.accessToken = this.oauthService.getAccessToken();
    this.runRequest(`${environment.apiUrl}/Country`);
  }

  private runRequest(url: string) {
      this.httpClient.get<countryResponseDto[]>(url, {
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        }
      }).subscribe((response: countryResponseDto[]) => {
        this.people$ = of(response);
      });
}
}