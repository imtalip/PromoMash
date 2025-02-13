import {Component, Inject, OnInit} from '@angular/core';
import {OAuthService} from "angular-oauth2-oidc";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {countryResponseDto} from './../../../interfaces/countryResponseDto';
import {Observable} from 'rxjs'
import { FormControl } from '@angular/forms';
import { NgLabelTemplateDirective, NgOptionTemplateDirective, NgSelectComponent, NgSelectConfig } from '@ng-select/ng-select';

@Component({
  selector: 'app-api-call',
  templateUrl: './api-call.component.html',
  styleUrls: ['./api-call.component.css'],
  standalone: false
})
export class ApiCallComponent implements OnInit {
  selectedCar: number | undefined;

  cars = [
      { id: 1, name: 'Volvo' },
      { id: 2, name: 'Saab' },
      { id: 3, name: 'Opel' },
      { id: 4, name: 'Audi' },
  ];

  countries: any;  
  country = new FormControl('');
  accessToken = '';
  response = '';

  constructor(private oauthService: OAuthService, private httpClient: HttpClient, private config: NgSelectConfig) {
      this.config.notFoundText = 'Custom not found';
      this.config.appendTo = 'body';
      // set the bindValue to global config when you use the same 
      // bindValue in most of the place. 
      // You can also override bindValue for the specified template 
      // by defining `bindValue` as property
      // Eg : <ng-select bindValue="some-new-value"></ng-select>
      this.config.bindValue = 'value';
  }

  ngOnInit(): void {
    this.accessToken = this.oauthService.getAccessToken();
  }

  async getCountry() {
    this.countries = await this.runRequest(`${environment.apiUrl}/Country`);
  }

  private runRequest(url: string) {
    return new Promise<countryResponseDto[]>((resolve) => {
      this.httpClient.get<countryResponseDto[]>(url, {
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        }
      }).subscribe(response => {
        resolve(response);
      });
    })
  }
}
