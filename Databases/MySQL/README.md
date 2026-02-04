# MySQL Environment Variables

## Configuration Variables

The following environment variables are used to configure the MySQL service:

| Variable | Purpose |
|----------|---------|
| `MYSQL_DATABASE` | Name of the database to create on initialization |
| `MYSQL_USER` | Username for database access |
| `MYSQL_PASSWORD` | Password for the database user |
| `MYSQL_ROOT_PASSWORD` | Password for the MySQL root user |
| `PMA_HOST` | phpMyAdmin host connection |
| `PMA_USER` | phpMyAdmin database user |
| `PMA_PASSWORD` | phpMyAdmin database password |

## Setup Instructions

1. Create a `.env` file in the current folder (crud-employees-demo/Databases/MySQL/)
2. Define each variable with appropriate values
3. Load variables in your `docker-compose.yml` using `env_file` section (example provided in root repository directory)
4. Never commit credentials to version control. Don't remove `.env` from the respective `.gitignore` 