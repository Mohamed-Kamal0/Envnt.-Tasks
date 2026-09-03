// The single place the app learns WHERE the API lives.
//
// 5144 is the CatalogApi `http` profile in api/CatalogApi/Properties/launchSettings.json.
//
// IMPORTANT: Never put API keys or secrets here. This file is compiled into the JS bundle
// and shipped to every browser visitor. Secrets belong in server-side environment variables,
// read by a backend proxy that makes the upstream call — the browser never sees the token.
export const environment = {
  apiBase: "http://localhost:5144/api",
};
