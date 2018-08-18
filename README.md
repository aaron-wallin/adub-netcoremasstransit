# Masstransit / RabbitMQ / PCF / .NET Core

An example application that implements the following technologies:
* Masstransit [http://masstransit-project.com/]
* RabbitMQ [https://www.rabbitmq.com/]
* .NET Core [https://docs.microsoft.com/en-us/dotnet/core/]
* Pivotal Cloud Foundry [https://pivotal.io/platform]
* Lamar [https://jasperfx.github.io/lamar/]

## Notes

This is intended to be deployed to an instance of Pivotal Cloud Foundry and requires a RabbitMQ with a specific name bound to the application.  Additionally, due to file locking issues with Visual Studio, this also illustrates the usage of the .cfignore file to ignore files during the cf push.

## Implementation

* Create a PCF managed RabbitMQ named "masstransit-poc"
* Perform a `cf push` from the root folder of this repo (rename the application or other settings in the manifest as desired)
* Check the application log to see if the message consumer is working
