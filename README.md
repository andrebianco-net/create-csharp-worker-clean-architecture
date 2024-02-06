# create-csharp-worker-clean-architecture

## Overview:
Creating a C# .NET Core Worker based on Clean Architecture.

This example is an implementation where I adapted a Worker layer to work together clean architecture layers. Read [readme file ](https://github.com/andrebianco-net/andrebianco-net#readme) in order to obtain more details about the finality of this solution.

In order to know more about my career check my Linkedin profile, please.

https://www.linkedin.com/in/andrebianco-net/

## General Scope:

## How to run this project

#### 1. Clone project:

```bash
$ git clone https://github.com/andrebianco-net/create-csharp-worker-clean-architecture.git
```

#### 2. Update file appsettings.json with a valid:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Worker": {
    "Interval": 1000
  },
  "Serilog": {
    "Folder": "Log",
    "File": "ProductFeederServiceWorker.log",
    "Size": 5242880
  },
  "MongoDB": {
    "ConnectionURI": "YOUR MONGODB URI",
    "DatabaseName": "YOUR DATABASE NAME",
    "CollectionName": "Products"
  },  
  "API": {
    "UrlLoginUser": "http://localhost:5205/api/Token/LoginUser",
    "User": "YOUR API USER",
    "Password": "YOUR PASSWORD'S API USER",
    "UrlCategories": "http://localhost:5205/api/Categories",
    "UrlProducts": "http://localhost:5205/api/Products"
  }
}
```

#### 3. Compile project:

```bash
$ dotnet build
```

#### 4. Test project:

```bash
$ dotnet test
```

#### 5. Run project:

```bash
$ dotnet run --project ProductFeederService.Worker/ProductFeederService.Worker.csproj
```

#### 6. MongoDB document

As an example, the Json documents was created by [Data Ingestion Service](https://github.com/andrebianco-net/create-data-ingestion-python-mongodb) into Products collection.

```json
{
  "_id": {
    "$oid": "651c5dcf0c7cdeb0129d3ed7"
  },
  "category": "Racing Wheel",
  "name": "Advant Racing",
  "description": "Advant Racing Wheels for High Performance Cars",
  "price": 100,
  "stock": 10,
  "image": "https://image.made-in-china.com/43f34j00OIeYilgMAdkR/Advant-Racing-Wheels-for-High-Performance-Cars.jpg",
  "createdAt": "2023-10-03 15:30:39",
  "productUpdatedAt": "2023-10-03 15:31:14",
  "admissionResult": "OK. Serialization was realized successfully."
}
```

```json
{
  "_id": {
    "$oid": "651c5dcf0c7cdeb0129d3ed8"
  },
  "category": "Industrial Wheel",
  "name": "Forlong Wheel",
  "description": "Forlong Wheel Split Rim 3.00d-8 Et0 94/140/5 Tire 5.00-8 for Skid Loader and Industrial Equipment for Sale",
  "price": 50,
  "stock": 10,
  "image": "https://image.made-in-china.com/43f34j00mlGrykHIrJzv/Forlong-Wheel-Split-Rim-3-00d-8-Et0-94-140-5-Tire-5-00-8-for-Skid-Loader-Industrial-Equipment-for-Sale.jpg",
  "createdAt": "2023-10-03 15:30:39",
  "productUpdatedAt": "2023-10-03 15:31:15",
  "admissionResult": "OK. Serialization was realized successfully."
}
```

#### 7. Set the log folder

Define a real path to the log.

```json
"Serilog": {
    "Folder": "Log",
    "File": "ProductFeederServiceWorker.log",
    "Size": 5242880
},
```

#### 8. From Docker to Azure Container

```bash
$ docker build -t product-feeder-service .
```

```bash
[$ docker run -it product-feeder-service]
```

```bash
$ docker login youruricreatedinazurecontainerregistry.azurecr.io
```

```bash
$ docker tag product-feeder-service youruricreatedinazurecontainerregistry.azurecr.io/product-feeder-service
```

```bash
$ docker push youruricreatedinazurecontainerregistry.azurecr.io/product-feeder-service
```

```bash
[$ docker pull youruricreatedinazurecontainerregistry.azurecr.io/product-feeder-service]
```

#### 9. Created in Azure Cloud Computing
###
![image](https://github.com/andrebianco-net/create-csharp-worker-clean-architecture/assets/453193/492804b2-ab89-4402-b496-48790af88a67)

###
![image](https://github.com/andrebianco-net/create-csharp-worker-clean-architecture/assets/453193/ed99bdc2-6836-4633-9e10-69345f9f2171)

###
![image](https://github.com/andrebianco-net/create-csharp-worker-clean-architecture/assets/453193/d7c683b7-55aa-43ed-b3a8-f55b85365fb7)


