# create-csharp-worker-clean-architecture

#### 1. From Docker to Azure Container

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

#### 2. Created in Azure Cloud Computing
###
![image](https://github.com/andrebianco-net/create-csharp-worker-clean-architecture/assets/453193/492804b2-ab89-4402-b496-48790af88a67)

###
![image](https://github.com/andrebianco-net/create-csharp-worker-clean-architecture/assets/453193/ed99bdc2-6836-4633-9e10-69345f9f2171)

