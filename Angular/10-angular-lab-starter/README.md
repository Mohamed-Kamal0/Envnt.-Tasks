> 🚧 **This is the STUDENT STARTER version** — AuthService, the route guard, and the login form are intentionally incomplete. Follow the `#Task N` comments in the listed files. See the sibling `solution/` folder for the completed reference solution if you get stuck.

> AuthService, the auth guard, the login page, and the dashboard page don't exist yet in this project — you create them from scratch. `ng serve` will show missing-file/module errors until they exist; that's expected.

> `AuthService` talks to a real backend: `POST /api/auth/login`, forwarded to your .NET API by `proxy.conf.json`. Start the API before you try to sign in — with it down, every login attempt simply fails.

## 🎯 Your Tasks

- [ ] **#Task 1** — **Path:** `src/app/services/auth.service.ts`. This file does not exist yet — create it with `ng generate service services/auth`, then implement it (Tasks 1–4 below all live in this same file). Start by initializing `isAuthenticated` as a writable signal whose starting value comes from whether a session marker already exists in `localStorage`, so a page refresh doesn't log the user out.
- [ ] **#Task 2** — **Path:** `src/app/services/auth.service.ts`. Implement `login()` — send the credentials to the backend's login endpoint (`environment.apiUrl` + `/auth/login`) and resolve the result to a success/failure boolean, handling request failures gracefully instead of throwing.
- [ ] **#Task 3** — **Path:** `src/app/services/auth.service.ts`. Implement `logout()` to clear the persisted session marker and set the authentication signal back to signed-out.
- [ ] **#Task 4** — **Path:** `src/app/services/auth.service.ts`. Implement the private `markLoggedIn()` helper to persist the session marker and set the authentication signal to signed-in.
- [ ] **#Task 5** — **Path:** `src/app/guards/auth.guard.ts`. This file does not exist yet — create it with `ng generate guard guards/auth` (the generator will ask "which type of guard would you like to create?" — choose **CanActivate**, which is the type this guard needs). Then implement the real authentication check, redirecting unauthenticated users back to the login page instead of granting access to the protected route.
- [ ] **#Task 6** — Create and implement the Login component. **Path:** `src/app/pages/login/login.component.ts` and `src/app/pages/login/login.component.html`. Neither file exists yet — run `ng generate component pages/login --standalone` to scaffold them, then build a **reactive** login form: import `ReactiveFormsModule` into the component, inject `FormBuilder`, and build a `FormGroup` holding an `email` control and a `password` control. Every validation rule lives in TypeScript — the email control is required and must look like an email address, the password control is required and needs at least six characters — so the template's inputs carry only a `formControlName`, no `required`/`minlength` attributes and no `name` attribute. Bind the form with `[formGroup]`, show a per-field hint once a control is invalid and touched, and disable the submit button while the form is invalid or a submission is already in progress. Then implement the component's `onSubmit()` handler so that it bails out early on an invalid form, and otherwise flips a submitting flag on and clears any previous error message, reads the email and password off the form's value, calls into `AuthService` to attempt a login, and reacts to the result — moving the user on to the dashboard page on success, or showing a message that the email or password was incorrect on failure — flipping the submitting flag back off once the attempt finishes either way.
- [ ] **#Task 7** — Create and implement the Dashboard component. **Path:** `src/app/pages/dashboard/dashboard.component.ts` and `src/app/pages/dashboard/dashboard.component.html`. Neither file exists yet — run `ng generate component pages/dashboard --standalone` to scaffold them. This is a placeholder page reachable only through `authGuard`, whose job is simply to confirm the user reached it with a valid session — it should say something to the effect that the user got there because `authGuard` found a valid session, and note that opening the URL in a private/signed-out window bounces back to `/login` instead (Day 5 fills this page in with the real product management screens). Inject `AuthService` and `Router` into the component and add a `logout()` method that calls into the authentication service and navigates back to the login page, then bind the template's "Sign out" button click event to that `logout()` method.

---

# Day 04 — Login, AuthService & Route Guard

## Hands-on

1. **Account Pages Generation** — `LoginComponent` and `DashboardComponent`.
2. **Interactive Credentials Interface** — `login.component.html` with an email
   input and a password input, both driven by a `FormGroup` through
   `formControlName` (Reactive Forms).
3. **Authentication Backend Service** — `AuthService.login()` sends credentials
   to the server, toggles a session-state signal, and caches a login marker in
   the browser.
4. **Navigation Router Protection Guard** — a functional `authGuard`
   (`ng g guard guards/auth --functional`) that asks `AuthService` for
   clearance.
5. **Locking Access & Conditional Forwarding** — `{ path: 'dashboard', ...,
   canActivate: [authGuard] }`. A direct, unauthenticated hit on `/dashboard`
   is blocked, shows an alert, and is redirected to `/login`.

## Run it

```bash
npm install
npm start
```

`npm start` runs `ng serve --proxy-config proxy.conf.json`, so `/api/*` reaches
the .NET backend at `https://localhost:7297`. Have that API running — the login
form posts to it for real.

## The flow described in the brief, exactly as built

> A login page is created containing an email and password. When the login
> button is clicked, a request is sent to the API. If the data is correct,
> authentication is enabled, the user's status is saved, and routing guards are
> used to prevent any unlogged user from proceeding to the dashboard.

That is `LoginComponent` → `AuthService.login()` → `authGuard` on the
`/dashboard` route, in that order.

## Try it

1. Log in from `/login`, then open `/dashboard` in a new **private/incognito**
   window — you are redirected, because the session marker lives in that
   window's own `localStorage`.
2. Refresh the dashboard page after logging in — you stay signed in, because
   `AuthService` reads `localStorage` when the service is constructed.
3. Type a malformed email or a 3-character password — the submit button stays
   disabled, because the `FormGroup`'s validators say the form is invalid. No
   request leaves the browser.

## Try it yourself

1. Add a `returnUrl` query param so the guard sends the user back to the page
   they originally asked for, not always `/dashboard`.
2. Replace the `window.alert` in `authGuard` with a nicer in-page banner.
3. Add a `logout()` button to the navbar (the dashboard already has one).
