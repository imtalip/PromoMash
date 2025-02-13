import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentUrlService {
  public urlBackAddress: string = environment.urlBackAddress;
  public urlCountry: string = environment.urlCountry;
  constructor() { }
}
