import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [/* your components */],
  imports: [
    HttpClientModule,  // Add this line to import the HttpClientModule
    BrowserModule,     // You will most likely need this for your Angular app to work
    /* other modules */
  ],
  providers: [],
  bootstrap: [/* your main component */]
})
export class AppModule { }


