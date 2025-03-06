import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { AppRoutingModule } from "./app.routes";
import { AppComponent } from "./app.component";

@NgModule({
  declarations: [],
  imports: [BrowserModule, FormsModule, HttpClientModule, AppRoutingModule  ],
  bootstrap: [],
})
export class AppModule {}
