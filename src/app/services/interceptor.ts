import { HttpEvent, HttpHandler, HttpInterceptor } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class AuthenticationInterceptors implements HttpInterceptor {
  intercept(req: any, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem("jwtToken");
    if (token) {
      const cloned = req.clone({
        headers: req.headers.set("Authorization", `Bearer ${token}`),
      });
      return next.handle(cloned);
    } else {
      return next.handle(req);
    }
  }
}
