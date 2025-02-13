import {Component, Inject, OnInit} from '@angular/core';
import {Title} from "@angular/platform-browser";
import {ActivatedRoute, Router} from "@angular/router";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
  standalone: false
})
export class MainComponent implements OnInit{

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
  }

    async login() {
      
      let redirectUrl = '/country/home';

      this.router.navigate([redirectUrl]);
    }

    async register() {
      window.location.href = environment.registryUrl;
    }
  
}
