// main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { HttpClientModule } from '@angular/common/http';

bootstrapApplication(AppComponent, {
  providers: [
    HttpClientModule  // Ensure HttpClientModule is provided here
  ]
}).catch((err) => console.error(err));
