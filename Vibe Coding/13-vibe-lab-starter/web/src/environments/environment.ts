// The single place the app learns WHERE the API lives. Every service builds its
// URLs from environment.apiBase, so the port changes in one spot.
//
// 5144 is the CatalogApi `http` profile in api/CatalogApi/Properties/launchSettings.json.
// If you start the API with the `https` profile instead, use https://localhost:7021/api.
export const environment = {
  apiBase: "http://localhost:5144/api",
};
