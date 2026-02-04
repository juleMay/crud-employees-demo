# Environment Variables

## Configuration Variables

The following environment variables are required to run the .NET Web API:

| Category | Variable | Description |
|----------|----------|-------------|
| Logging | `Logging__LogLevel__Default` | Default logging level (e.g., "Information") |
| Logging | `Logging__LogLevel__Microsoft.AspNetCore` | ASP.NET Core logging level (e.g., "Warning") |
| Database | `ConnectionString` | MySQL database connection string |
| Authentication | `Auth__ProjectId` | Firebase project ID |
| Authentication | `Auth__KeysUrl` | URL for retrieving public keys |
| Authentication | `Auth__AdditionalKeysUrl` | Additional keys URL for token validation |
| Authentication | `Auth__IncludeErrorDetails` | Enable detailed error messages (true/false) |
| Authentication | `Auth__Authority` | Token issuer authority URL |
| Authentication | `Auth__ValidIssuer` | Expected token issuer |
| Authentication | `Auth__ValidateIssuer` | Validate issuer claim (true/false) |
| Authentication | `Auth__ValidateAudience` | Validate audience claim (true/false) |
| Authentication | `Auth__ValidateLifetime` | Validate token expiration (true/false) |
| Authentication | `Auth__ValidateIssuerSigningKey` | Validate signing key (true/false) |
| Hosting | `AllowedHosts` | Comma-separated list of allowed hosts (e.g., "*") |

## Setup Instructions

1. Create a `.env` file in the current folder (crud-employees-demo/Services/WebApi)
2. Define each variable with appropriate values
3. Load variables in your `docker-compose.yml` using `env_file` section (example provided in root repository directory)
4. Never commit credentials to version control. Don't remove `.env` from the respective `.gitignore` 