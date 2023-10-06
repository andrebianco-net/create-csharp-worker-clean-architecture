# create-csharp-worker-clean-architecture

## Overview:
Creating a C# .NET Core Worker based on Clean Architecture.

This example is an implementation where I adapted a Worker layer to work together clean architecture layers. Read [readme file ](https://github.com/andrebianco-net/andrebianco-net#readme) in order to obtain more details about the finality of this solution.

In order to know more aboute my career check my Linkedin profile, please.

https://www.linkedin.com/in/andrebianco-net/

## General Scope:

Product Feeder Service implementation propose a small example of how to create a Worker service using C#.

C# .NET Core Worker based on Clean Architecture, using Worker, Repository and xUnit to test the implementation. It will use MongoDB as a database for consume and a Web API to serialize the data processed.

The Solution will be a Worker which read a MongoDB queue (Product collection) and after validate each doc it will be stored in another base via a Web API.

## How to run this project

#### 1. Clone project:

$ git clone https://github.com/andrebianco-net/create-csharp-worker-clean-architecture.git

#### 2. Update file appsettings.json with a valid:

First rename the file appsettings.template to appsettings.json.

"MongoDB": {
    "ConnectionURI": "YOUR MONGODB URI",
    "DatabaseName": "YOUR DATABASE NAME",
    ...
}

"API": {
    ...
    "User": "YOUR API USER",
    "Password": "YOUR PASSWORD'S API USER",
    ...
}

#### 3. Compile project:

$ dotnet build

#### 4. Test project:

$ dotnet test

#### 5. Run project:

$ dotnet run --project ProductFeederService.Worker/ProductFeederService.Worker.csproj
