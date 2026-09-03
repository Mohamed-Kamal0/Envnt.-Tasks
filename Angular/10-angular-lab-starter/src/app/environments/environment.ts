export const environment = {
  production: false,

  /**
   * DAY 4 — requests to /api are forwarded to your .NET backend by
   * proxy.conf.json (see the `start` script in package.json), so
   * AuthService posts the credentials to /api/auth/login.
   */
  apiUrl: "http://localhost:5144/api",
  useMockAuth: true,
};
