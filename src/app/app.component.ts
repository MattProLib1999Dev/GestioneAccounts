import { Component } from '@angular/core';
import { Account } from './modal/account/account.component';
import { AggiungiAccountComponent } from './modal/aggiungi-account/aggiungi-account.component';
import { AggiungiValoreComponent } from './modal/aggiungi-valore/aggiungi-valore/aggiungi-valore.component';
import { JwtAuth } from './models/jwtAuth';
import { Login } from './models/login';
import { Register } from './models/register';
import { AuthenticationService } from './services/authentication.service';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [Account, AggiungiAccountComponent, AggiungiValoreComponent, FormsModule],
})
export class AppComponent {
  title = 'Gestione Accounts';
  loginDto = new Login();
  registerDto = new Register();
  jwtAuth: JwtAuth = new JwtAuth();
  isLoggedIn: boolean = false;

  constructor(private authService: AuthenticationService) {}

  register(registerDto: Register) {
    this.authService.register(registerDto).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
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
}
