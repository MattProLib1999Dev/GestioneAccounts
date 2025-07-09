import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from "./app.routes";
import { AppComponent } from "./app.component";
import { AuthenticationInterceptors } from "./services/interceptor";
import { Account } from "./modal/account/account.component";
import { CommonModule } from "@angular/common";

@NgModule({
  declarations: [

  ],
  imports: [
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    NgModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptors,
      multi: true
    }
  ]
})
export class AppModule {}
