# Student tasks — Week 2 · Day 10: Login, AuthService & a Route Guard

**Today's goal:** build the whole sign-in chain yourself — a service holding the session, a login
form that uses it, and a guard that turns "signed in" into "allowed through the door". **You'll
need:** this day's `starter`; your API's `POST /api/auth/login` running, because `AuthService`
posts to it for real — there is no simulated login mode.

**Manual-first:** weeks 1–2 are hands-by-you. AI may explain a concept or an error message — it
does not write your auth. Any line you can't explain, you redo (the
[JUDGING.md](../../../JUDGING.md) rule).

**Reference solution:** after you've done the manual-first work, check yours against `solution`
(`starter` is where you begin).

## Before you start
- [ ] `cd starter && npm install && npm start` runs; the app is day 9's, with no login anywhere.
- [ ] Your .NET API is running and exposes `POST /api/auth/login`; `proxy.conf.json` forwards
      `/api` to it. With the API down, every sign-in attempt fails.
- [ ] You've read the seven `#Task` comments in the starter.

## Tasks

### 1 · AuthService, and the signal that survives a refresh  ⏱ ~20
Fill in `services/auth.service.ts` — it's stubbed with the `#Task` comments. Give it an
`isAuthenticated` writable signal whose **starting value comes from `localStorage`**, so a page
refresh doesn't sign you out.
**Done when:** the signal reads `true` on construction if the marker is already there.

### 2 · login() against the API  ⏱ ~25
Implement `login()`: post the credentials to `environment.apiUrl + '/auth/login'`, resolve to a
success/failure boolean, and handle a failed request without throwing at the caller.
**Done when:** correct credentials sign you in against your own API — and a wrong password comes
back as a clean `false`, not an unhandled error.

### 3 · Logout, and the helper login() leans on  ⏱ ~10
Implement `logout()` (clear the marker, set the signal to signed-out) and the private
`markLoggedIn()` (persist the marker, set the signal to signed-in).
**Done when:** `login()` goes through `markLoggedIn()` — the marker is written in exactly one
place.

### 4 · The guard  ⏱ ~25
Fill in `guards/auth.guard.ts` (stubbed for you as a `CanActivate` guard), and
implement the real check: signed in passes, signed out is sent to `/login`. Attach it to the
dashboard route.
**Done when:** `/dashboard` typed into the address bar while signed out lands you on `/login`.

### 5 · The login page — a reactive form  ⏱ ~40
Fill in `pages/login/` (the component is stubbed for you) and build a **reactive** form:
`ReactiveFormsModule` imported, `FormBuilder` injected, and a `FormGroup` with an `email` control
and a `password` control. Put the rules in the group — `Validators.required` + `Validators.email`
on the email, `Validators.required` + `Validators.minLength(6)` on the password — and give each
input nothing but a `formControlName`. Bind the form with `[formGroup]`, show a field's message
once it is invalid **and** touched, and disable the submit button while the form is invalid or a
submit is already running. Then write `onSubmit()` — return early if the form is invalid;
otherwise flag on, clear the old error, read the values off the form, call `AuthService`, and
either navigate to the dashboard or show "email or password was incorrect", flag off either way.
**Done when:** submitting twice quickly cannot fire two logins, an invalid form never sends a
request, and a failed attempt leaves you on the page with a message.
Stuck? A `formControlName` that doesn't match a key in the `FormGroup` throws at runtime — read the
error, it names the missing control. And don't add `required`/`minlength` to the inputs as well:
those directives register validators on a reactive control too, so you'd have the same rule in two
places, drifting apart the first time you change one.

### 6 · The dashboard placeholder  ⏱ ~15
Fill in `pages/dashboard/` (stubbed for you): a page that only says it was reached through a valid session, plus a
"Sign out" button wired to a `logout()` method that clears the session and returns to `/login`.
Day 11 fills this page with the real product screens.
**Done when:** signing out from the dashboard sends you to `/login`, and going back to
`/dashboard` bounces you again.

### 7 · Prove it  ⏱ ~10
Sign in, then open `/dashboard` in a private window. Then sign in again and refresh.
**Done when:** the private window redirects (its own `localStorage` is empty) and the refresh
keeps you signed in — and you can say why, out loud, in one sentence each.

## Verify

```bash
cd starter
npm start
```

Then: `/dashboard` signed out → `/login` · sign in → dashboard · refresh → still in · sign out →
`/login`.

## End-of-day deliverables
- [ ] `AuthService` with a signal seeded from `localStorage`, an API-backed `login()`, `logout()`
      and one `markLoggedIn()`
- [ ] A `CanActivate` guard protecting `/dashboard`, redirecting instead of blank-screening
- [ ] A reactive login form: validators in the `FormGroup`, button disabled while invalid or
      submitting, readable error on failure
- [ ] Dashboard placeholder with a working sign-out
- [ ] Every line explained ([JUDGING.md](../../../JUDGING.md))

## Finished early?
- Add a `returnUrl` query param so the guard sends the user to the page they actually asked for.
- Replace any `alert()` with an in-page banner.
- A dead API and a wrong password currently look identical to the user ("email or password was
  incorrect"). Tell them apart in `login()` — the `HttpErrorResponse` status is `0` when the
  request never reached a server — and show the right message for each.
- Write two sentences on why a `localStorage` marker is not the same thing as being authorised by
  the server — and what the API must still check.
