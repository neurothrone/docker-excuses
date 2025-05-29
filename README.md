# Docker Excuses

## Setup

```shell
docker build -f ./DockerExcuses.WebApi/Dockerfile -t neurothrone/docker-excuses-webapi .

az login
az acr login --name zaneregistry

docker tag neurothrone/docker-excuses-webapi:latest zaneregistry.azurecr.io/neurothrone/docker-excuses-webapi:latest
docker push zaneregistry.azurecr.io/neurothrone/docker-excuses-webapi:latest
```