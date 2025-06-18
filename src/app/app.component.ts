import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { Account } from './modal/account/account.component';
import { AggiungiAccountComponent } from './modal/aggiungi-account/aggiungi-account.component';
import { AggiungiValoreComponent } from './modal/aggiungi-valore/aggiungi-valore/aggiungi-valore.component';
import { JwtAuth } from './models/jwtAuth';
import { Login } from './models/login';
import { Register } from './models/register';
import { AuthenticationService } from './services/authentication.service';
import { FormsModule } from '@angular/forms';
import { isPlatformBrowser, NgIf } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [Account, FormsModule],
})
export class AppComponent implements OnInit {
  title = 'Gestione Accounts';
  loginDto = new Login();
  registerDto = new Register();
  jwtAuth: JwtAuth = new JwtAuth();
  isLoggedIn: boolean = false;
  isRegistered: boolean = false;

  constructor(
    private authService: AuthenticationService,
    @Inject(PLATFORM_ID) private platformId: Object // ⬅️ AGGIUNTO
  ) {}

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      const token = localStorage.getItem('jwtToken');
      if (token) {
        this.isLoggedIn = true;
        this.jwtAuth.token = token;
      }
    }
  }

  register(registerDto: Register) {
    this.authService.register(registerDto).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
        this.isRegistered = false;
      },
      error: (error) => {
        console.error('Registration failed', error);
      },
    });
  }

  login(loginDto: Login) {
    this.authService.login(loginDto).subscribe((jwtDto) => {
      localStorage.setItem('jwtToken', jwtDto.token);
      this.isLoggedIn = true;
    })
  }

  toggleRegistrationForm(): void {
    this.isRegistered = !this.isRegistered;
  }
}
