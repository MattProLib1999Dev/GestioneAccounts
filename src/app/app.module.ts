import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { AppRoutingModule } from "./app.routes";
import { AppComponent } from "./app.component";
import { AuthenticationInterceptors } from "./services/interceptor";

@NgModule({
  declarations: [],
  imports: [BrowserModule, FormsModule, HttpClientModule, AppRoutingModule, AppComponent  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthenticationInterceptors,
    multi: true
  }],
  bootstrap: [],
})
export class AppModule {}
