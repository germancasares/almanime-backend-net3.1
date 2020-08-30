[TODO: Add stickers]

# Table of contents

  * [Add the configuration files](#add-the-configuration-files)
    * [Configure the Presentation project](#configure-the-presentation-project)
    * [Configure the Jobs project](#configure-the-jobs-project)
    * [Configure the Docker Compose Override](#configure-the-docker-compose-override)
    * [Configure the environment variables](#configure-the-environment-variables)
  * [Run the migrations](#run-the-migrations)
  * [Apendix](#apendix)
    * [Note on environment variables for the Jobs project](#note-on-environment-variables-for-the-jobs-project)

# Add the configuration files

## Configure the Presentation project

1. Create a json file on the root of the Presentation project, with the name `appsettings.json`
2. Add this snippet:
    ```JSON5
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*",
      "SwaggerOptions": {
        "Doc": {
          "Description": "Backend for the Almanime project.",
          "Email": "german.casares@outlook.com",
          "Name": "German Casares March",
          "Title": "AlmBackend API",
          "Url": "https://www.linkedin.com/in/germancasares/",
          "Version": "v1"
        },
        "SecurityRequirement": {
          "Id": "Bearer",
          "In": "Header",
          "Name": "Bearer",
          "Scheme": "oauth2",
          "Type": "SecurityScheme"
        },
        "SecurityDefinition": {
          "AuthorizationUrl": "https://authorization.com",
          "Description": "Please Enter Authentication Token",
          "In": "Header",
          "Name": "Authorization",
          "RefreshUrl": "https://refresh.com",
          "Scheme": "Bearer",
          "TokenUrl": "https://token.com",
          "Type": "ApiKey"
        },
        "UI": {
          "EndpointName": "AlmBackend API V1",
          "EndpointUrl": "/swagger/v1/swagger.json",
          "RoutePrefix": ""
        }
      }
    }
    ```
3. Create a json file on the root of the Presentation project, with the name `appsettings.Development.json` (This will be used on your development environment)
4. Add this snippet to the file and edit accordingly:
    ```JSON5
    {
      "ConnectionStrings":{
        // Connection to the Azure Storage, for localhost use UseDevelopmentStorage=true
        "AzureWebJobsStorage":"",
        // Server=securitydb;Database=master;User=sa;Password=<The SA_PASSWORD_SECURIRTYDB environment variable from .env in the Docker Compose>
        "SecurityConnection":"",
        // Server=datadb;Database=master;User=sa;Password=<The SA_PASSWORD_DATADB environment variable from .env in the Docker Compose>
        "AlmanimeConnection":""
      },
      "FrontendOptions": {
        "Urls": [
          // List of urls of the frontends, this is a list to allow the local and deployed frontends to use this backend
          // Example: "http://localhost:8080"
        ]
      },
      "TokenOptions":{
        // Recipient for which the JWT is intended, an url
        "Audience":"",
        // Issuer of the JWT, an url
        "Issuer":"",
        // Key that will be used to encrypt the tokens, has to be 128 bytes long
        "Secret":"",
        // Days that the access token will last
        "AccessExpirationDays": "",
        // Days that the refresh token will last
        "RefreshExpirationDays": ""
      }
    }
    ```
5. Include both files on the output directory, `Copy to Output Directory: Copy if newer`

## Configure the Jobs project 

1. Create a json file on the root of the Jobs project, with the name `local.settings.json`
2. **[NOTE](#note-on-environment-variables-for-the-jobs-project)** Add this snippet to the file:
    ```JSON5
    {
      "IsEncrypted": false,
      "Values": {
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "ConnectionStrings:AzureWebJobsStorage": "UseDevelopmentStorage=true",
        // Server=datadb;Database=master;User=sa;Password=<The SA_PASSWORD_DATADB environment variable from .env in the Docker Compose>
        "AlmanimeConnection": ""
      }
    }
	  ```

## Configure the Docker Compose Override

1. Create a yml file on the root of the solution, with the name `docker-compose.override.yml`
2. Add this snippet to the file:
    ```YML
    version: '3.8'

    services:
      jobs:
        environment:
          - AZURE_FUNCTIONS_ENVIRONMENT=Development
          - ASPNETCORE_URLS=http://+:80
        ports:
          - "7071:80"
        env_file:
          - jobs.env

      api:
        environment:
          - ASPNETCORE_ENVIRONMENT=Development
          - ASPNETCORE_URLS=https://+:443;http://+:80
        ports:
          - "433:443"
        volumes:
          - ${APPDATA}/Microsoft/UserSecrets:/root/.microsoft/usersecrets:ro
          - ${APPDATA}/ASP.NET/Https:/root/.aspnet/https:ro

      data_db:
        ports:
          - "1433:1433"
        env_file:
          - db.env

      security_db:
        ports:
          - "1434:1433"
        env_file:
          - db.env
    ```

## Configure the environment variables
3. Add a db.env file to the root of the project, with the following snippet:
    ```sh
    # A secure password for the database
    SA_PASSWORD=
    ACCEPT_EULA=Y
    ```
4. **[NOTE](#note-on-environment-variables-for-the-jobs-project)** Add a jobs.env file to the root of the project, with the following snippet:
    ```sh
    # A secure password for the database
    AzureWebJobsStorage=UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://host.docker.internal
    AzureWebJobsDashboard=UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://host.docker.internal
    ConnectionStrings:AzureWebJobsStorage=UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://host.docker.internal
    # Use this for the next value: Server=datadb;Database=master;User=sa;Password=<The SA_PASSWORD_DATADB environment variable from .env in the Docker Compose>
    AlmanimeConnection=
    ```
5. Build the Docker Compose project, so the images are created.

# Run the migrations

1. Run the Docker Compose project, so the containers are created.
2. Target the `Presentation` project, open the Package Manager Console and set Default Project to `06.Migrations\Migrations.Security`
3. Modify the `SecurityConnection` on `appsettings.Development.json` to `Server=localhost,1434;...`
4. Run the command `Update-Database -Context SecurityContext`
5. Modify the `SecurityConnection` on `appsettings.Development.json` to `Server=securitydb;...`
6. Target the `Presentation` project, open the Package Manager Console and set Default Project to `06.Migrations\Migrations.Data`
7. Modify the `SecurityConnection` on `appsettings.Development.json` to `Server=localhost,1433;...`
8. Run the command `Update-Database -Context AlmanimeContext`
9. Modify the `SecurityConnection` on `appsettings.Development.json` to `Server=datadb;...`

# Appendix

## Note on environment variables for the Jobs project

For the Jobs project, since there is not possible to debug an Azure function on a Docker container, both sets of environment variables is included. The one on the local.settings.json (Which will be used for debugging) and the jobs.env (Which will be used at runtime). For this very reason, it is recommendable to update both files at the same time so the environment variables are synced.