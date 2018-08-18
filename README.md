# Masstransit / RabbitMQ / PCF / .NET Core

An example application that implements the following technologies:
* Masstransit [http://masstransit-project.com/]
* RabbitMQ [https://www.rabbitmq.com/]
* .NET Core
* Pivotal Cloud Foundry [https://pivotal.io/platform]

## Notes

This is intended to be deployed to an instance of Pivotal Cloud Foundry and requires a RabbitMQ with a specific name bound to the application.  Additionally, due to file locking issues with Visual Studio, this also illustrates the usage of the .cfignore file to ignore files during the cf push.
