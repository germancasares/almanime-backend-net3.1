# AlmBackend

Backend of the Almanime website

## Configuration files

### Configuration for the Presentation project

1. Create a json file on the root of the Presentation project, with the name `appsettings.json`
2. Add this block of text to the file:
	```JSON
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
4. Add this block of text to the file and edit accordingly:
    ```JSON
    {
       "ConnectionStrings":{
          "AzureWebJobsStorage":"<Connection to the Azure Storage, for localhost use UseDevelopmentStorage=true>",
          "SecurityConnection":"Server=securitydb;Database=master;User=sa;Password=<The SA_PASSWORD_SECURIRTYDB environment variable from .env in the Docker Compose>",
          "AlmanimeConnection":"Server=datadb;Database=master;User=sa;Password=<The SA_PASSWORD_DATADB environment variable from .env in the Docker Compose>"
       },
       "FrontendOptions": {
          "Urls": [
            "http://localhost:8080",
            "<List of urls of the frontends, this is a list to allow the local and deployed frontends to use this backend>"
          ]
       },
       "TokenOptions":{
          "Audience":"<Recipient for which the JWT is intended, an url>",
          "Issuer":"<Issuer of the JWT, an url>",
          "Secret":"<Key that will be used to encrypt the tokens, has to be 128 bytes long>",
          "AccessExpirationDays": "<Days that the access token will last>",
          "RefreshExpirationDays": "<Days that the refresh token will last>"
       }
    }
    ```
5. Include both files on the output directory, `Copy to Output Directory: Copy if newer`
7. Build the Docker Compose project, so the images are created.
8. Add a .env file to the Docker Compose project, with the following sections:
    ```
    SA_PASSWORD_DATADB=<A secure password for the Data database>
    SA_PASSWORD_SECURIRTYDB=<A secure password for the Security database>
    ```



### Run the migrations

1. Run the Docker Compose project, so the containers are created.
2. Target the `Presentation` project, open the Package Manager Console and set Default Project to `06.Migrations\Migrations.Security`
3. Run the command `Update-Database`
4. Target the `Presentation` project, open the Package Manager Console and set Default Project to `06.Migrations\Migrations.Data`
5. Run the command `Update-Database`



### Configuration for the Jobs project

1. Create a json file on the root of the Jobs project, with the name `local.settings.json`
2. Add this block of text to the file:
	```JSON
    {
        "IsEncrypted": false,
        "Values": {
            "FUNCTIONS_WORKER_RUNTIME": "dotnet",
            "AzureWebJobsStorage": "UseDevelopmentStorage=true",
            "ConnectionStrings:AzureWebJobsStorage": "UseDevelopmentStorage=true",
            "AlmanimeConnection": "Server=datadb;Database=master;User=sa;Password=<The SA_PASSWORD_DATADB environment variable from .env in the Docker Compose>"
        }
    }
	```



### Configuration for the Docker Compose Override

1. Create a jml file on the root of the solution, with the name `docker-compose.override.yml`
2. Add this block of text to the file:
    ```YML
    version: '3.8'

    services:
        jobs:
            environment:
                - AZURE_FUNCTIONS_ENVIRONMENT=Development
                - ASPNETCORE_URLS=http://+:80
            ports:
                - "7071:80"

        backend:
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
    
        security_db:
            ports:
                - "1434:1433"
    ```