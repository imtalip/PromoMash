import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: false
})
export class AppComponent {
  title = 'PromoMashRegisterClientUI';
  private roles: string[] = [];
  isLoggedIn = false;
  showAdminBoard = false;
  showModeratorBoard = false;
  username?: string;

  //constructor(private tokenStorageService: TokenStorageService) { }

  ngOnInit(): void {

  }

  logout(): void {
    window.location.reload();
  }
}
