---
name: endpoint-conventions
description: Use when the user asks to add, change, or review a .NET API endpoint, route, controller action, or API contract in this app. Apply this project's rules: DTOs at the boundary, thin controllers, business logic in services, and explicit API responses.
---

# Endpoint Conventions (Catalog API)

Apply these conventions whenever creating or changing endpoints in this codebase.

## Project Rules

1. Keep controllers thin:
   - Parse HTTP input.
   - Call a service interface.
   - Map entities to DTOs.
   - Return HTTP results.
2. Keep business logic in services, not controllers.
3. Keep entities inside the API layer; return DTOs only.
4. Preserve async controller actions and typed responses (`ActionResult<...>`).
5. Use explicit, meaningful status codes for failures (for example `NotFound`, `BadRequest`) when relevant.

## File Placement Pattern

- Controller actions: `api/CatalogApi/Controllers/`
- Service interface: `api/CatalogApi/Services/ICatalogService.cs` (or the relevant `I*Service`)
- Service implementation: `api/CatalogApi/Services/`
- Request/response DTOs: `api/CatalogApi/Dtos/`
- Domain entities: `api/CatalogApi/Models/`
- DI wiring: `api/CatalogApi/Program.cs`

## Implementation Checklist

When adding a new endpoint:

1. Define or update DTOs first (request/response contract).
2. Add method signature to the relevant service interface.
3. Implement the method in the concrete service class.
4. Add the controller action that calls the service and returns DTOs.
5. Keep entity-to-DTO mapping in the controller boundary.
6. Register new services in `Program.cs` only if DI setup changed.
7. Validate with targeted run/build:
   - `cd api/CatalogApi && dotnet run`
   - Exercise endpoint with `curl` or client call.

## Shape to Follow

Use this flow:

- `[Http...]` action receives query/body/route params
- call `_service.MethodAsync(...)`
- map result to DTO
- return `Ok(...)` (or another explicit status)

Avoid:

- Returning model entities directly from controller actions
- Duplicating business logic in controller methods
- Skipping service interface updates when adding new service behavior

