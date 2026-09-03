import { bootstrapApplication } from "@angular/platform-browser";
import { provideHttpClient } from "@angular/common/http";
import { App } from "./app/app";

// Standalone bootstrap — the Week 2 shape. One provider: HttpClient, because the
// catalog now comes from the .NET API instead of a hard-coded array.
bootstrapApplication(App, {
  providers: [provideHttpClient()],
}).catch((err) => console.error(err));
