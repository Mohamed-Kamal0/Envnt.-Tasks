import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { ApplicationConfig, provideZoneChangeDetection } from "@angular/core";
import { provideRouter, withComponentInputBinding } from "@angular/router";

import { routes } from "./app.routes";
import { authInterceptor } from "./interceptors/authInterceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),

    // #Task 1: Enable Angular's route-parameter-to-input binding feature so
    // that route params (such as the product id) are delivered directly as
    // component @Input() properties, instead of having to read them manually
    // from ActivatedRoute. This is what lets ProductDetailComponent and
    // ProductEditComponent receive their id purely through an input.

    // Solution: withComponentInputBinding
    provideRouter(routes, withComponentInputBinding()),
    provideHttpClient(withInterceptors([authInterceptor])),
  ],
};
