import { ApplicationConfig, provideZoneChangeDetection } from "@angular/core";
import { provideRouter } from "@angular/router";

import { routes } from "./app.routes";
import { provideHttpClient, withInterceptors } from "@angular/common/http";

/**
 * DAY 3, STEP 1 — HTTP Configuration.
 *
 * #Task 1: Wire up Angular's HttpClient for the whole application by adding
 * the appropriate provider function to the providers array below. Without
 * this, any service that injects HttpClient will throw a "no provider"
 * error at runtime.
 */
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
  ],
};
