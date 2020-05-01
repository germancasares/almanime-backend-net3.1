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
        "AllowedHosts": "*"
    }
	```
3. Create a json file on the root of the Presentation project, with the name `appsettings.Development.json` (This will be used on your development environment)
4. Add this block of text to the file:
    ```JSON
    {
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft": "Warning",
                "Microsoft.Hosting.Lifetime": "Information"
            }
        }
    }
    ```
5. Include both files on the output directory, `Copy to Output Directory: Copy if newer`
6. On the `Development` file, add the following sections:
    ```JSON
    {
        "FrontendUrls" : "<List of urls of the frontends, this is a list to allow the local and deployed frontends to use this backend>",
        "TokenConfiguration": {
            "Audience": "<Recipient for which the JWT is intended, an url>",
            "Issuer": "<Issuer of the JWT, an url>",
            "Secret": "<Key that will be used to encrypt the tokens, has to be 128 bytes long>",
            "AccessExpirationDays": "<Days that the access token will last>",
            "RefreshExpirationDays": "<Days that the refresh token will last>"
        },
        "ConnectionStrings": {
            "AzureStorage": "<Connection to the Azure Storage, use UseDevelopmentStorage=true for localhost>",
            "SecurityConnection": "<Connection to the Security Database, use Data Source=(localdb)\\ProjectsV13;Initial Catalog=Security;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False for localhost>",
            "AlmanimeConnection": "<Connection to the Almanime Database, use Data Source=(localdb)\\ProjectsV13;Initial Catalog=Almanime;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False for localhost>"
        }
    }
    ```
7. Target the `Presentation` project, open the Package Manager Console and set Default Project to `06.Migrations\Migrations.Security`
8. Run the command `Update-Database -Context SecurityContext`
9. Target the `Presentation` project, open the Package Manager Console and set Default Project to `06.Migrations\Migrations.Data`
10. Run the command `Update-Database -Context AlmanimeContext`


### Configuration for the Jobs project

1. Create a json file on the root of the Jobs project, with the name `local.settings.json`
2. Add this block of text to the file:
	```JSON
    {
        "IsEncrypted": false,
        "Values": {
            "AzureWebJobsStorage": "UseDevelopmentStorage=true",
            "FUNCTIONS_WORKER_RUNTIME": "dotnet"
        }
    }
	```
3. Include both files on the output directory, `Copy to Output Directory: Copy if newer`
6. Add the following sections:
    ```JSON
    {
        "Values": {
            "AlmanimeConnection": "<Connection to the Almanime Database, use Data Source=(localdb)\\ProjectsV13;Initial Catalog=Almanime;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False for localhost>"
        }
    }
    ```


## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
Give examples
```

### Installing

A step by step series of examples that tell you how to get a development env running

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc

