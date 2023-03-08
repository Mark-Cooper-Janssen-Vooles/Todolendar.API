# Todolendar.API

This repository is the backend for Todolendar, a project which is a todo list and calendar fusion.

The frontend for Todolendar is found at [Todolendar.UI](https://github.com/Mark-Cooper-Janssen-Vooles/Todolendar.UI).

Features include account creation, sign-in functionality, editing your profile, creating todo's and adding them into a calendar as a scheduled todo. Users will also be able to set SMS notifications for reminding them to plan their week / month / day, as well as for reminding them when they have a scheduled todo coming up.

## Planning
Figma designs are available [here](https://www.figma.com/file/ona2QoEu6QzTcyffAervOy/Todolender?node-id=0%3A1&t=KPdD8o2qc6cbYQnZ-0).

A basic database schema is available [here](https://app.diagrams.net/#G1NYqMTprbHGnyYW-6s-Pc1sLVT3hZQu_x).

## Technology 

This project is created as a .net web api, using entity framework, swagger, fluent validations, automapper, as well as Json Web Token.

## Architectural Patterns 

This project uses dependency injection and the repository pattern. It is an attempt at [onion architecture](https://www.codeguru.com/csharp/understanding-onion-architecture/).

The domain layer can be found in the Models folder, while the repository layer can be found in the repositories folder and the services layer can be found in the controllers folder. 

This project was inspired by a tutorial which can be found [here](https://github.com/Mark-Cooper-Janssen-Vooles/dotnet-web-api).

## Running Locally 

First you will need to create a database such as MSSQLSERVER, and set the connection string in appsettings.json. You will then need to run via the package manager console `Add-Migration` as well as `Update-Database` for entity framework to create the database connection.

If testing the endpoint via swagger, you will need to first create a user and then login. The login endpoint will give you a bearer you must authenticate with to use the other endpoints, each endpoint is accessibile only for that user. 