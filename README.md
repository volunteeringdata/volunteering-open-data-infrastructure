# x

## Transformation

The following creates RDF from JSON:

```zsh
dotnet run ./data/doit/activities.json ./fuseki/data.ttl --project ./transform/DoIt/
```

## Infrastructure

Azure Resource Manager (ARM) templates define the infrastructure (see contents of the `./infrastructure` folder).

The [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/) can be used to create the infrastructure.

### Pre-requisite

An [Azure Subscription](https://azure.microsoft.com/en-us/pricing/purchase-options/azure-account).

Login to [azure cli](https://learn.microsoft.com/en-us/cli/azure/) and load infrastructure environment variables.

```zsh
az login
source .env
```

### Azure Resource Group

A group used to manage related Azure resources.

```zsh
az group create --location $INFRASTRUCTURE_LOCATION --name $INFRASTRUCTURE_RESOURCE_GROUP_NAME
```

### Azure Container Registry

A container registry where images can be pushed and automatically deployed via webhooks.

```zsh
az deployment group create --resource-group $INFRASTRUCTURE_RESOURCE_GROUP_NAME --template-file ./infrastructure/azure-container-registry/template.json --parameters name=$INFRASTRUCTURE_CONTAINER_REGISTRY_NAME
```

### Azure App Service Plan

A set of compute resources for web apps to run.

```zsh
az deployment group create --resource-group $INFRASTRUCTURE_RESOURCE_GROUP_NAME --template-file ./infrastructure/app-service-plan/template.json --parameters name=$INFRASTRUCTURE_APP_SERVICE_PLAN_NAME
```

### Azure App Service: Data Container

A container-web-hosting service. `202510300952registry.azurecr.io/fuseki:latest`

```zsh
az deployment group create --resource-group $INFRASTRUCTURE_RESOURCE_GROUP_NAME --template-file ./infrastructure/app-service-data-container/template.json --parameters name=$INFRASTRUCTURE_DATA_CONTAINER_APP_SERVICE_NAME app_service_plan_name=$INFRASTRUCTURE_APP_SERVICE_PLAN_NAME
```

### Azure Container Registry Web Hook




## Data Container

A readonly triplestore with a sparql endpoint containing the latest data (data in `./fuseki/data.ttl`).

```zsh
docker build --build-arg JENA_VERSION=5.6.0 --tag $INFRASTRUCTURE_DATA_CONTAINER ./fuseki
```

```zsh
# Alternative dev
INFRASTRUCTURE_DATA_CONTAINER="202510300952registry.azurecr.io/fuseki"

docker build --build-arg JENA_VERSION=5.6.0 --tag 202510300952registry.azurecr.io/fuseki:latest ./fuseki
az acr login --name 202510300952registry
docker push 202510300952registry.azurecr.io/fuseki:latest
```

TODO: Try smaller or more optimized formats for RDF loading.

### Running the Data Container locally

SPARQL endpoint accessible at http://localhost:3030/sparql.

```zsh
docker compose build --build-arg JENA_VERSION=5.6.0
docker compose run --rm --service-ports fuseki
```

Example Query: `http://localhost:3030/sparql?query=CONSTRUCT { ?s ?p ?o } WHERE { ?s ?p ?o }`.

```http
sparql?query=CONSTRUCT { ?s <t> ?o } WHERE { ?s <https://id.example.org/schema/activityTitle> ?o }
```


### Deploy Data Container to Azure Container Registry

```zsh
az acr login --name $INFRASTRUCTURE_CONTAINER_REGISTRY_NAME
docker push $INFRASTRUCTURE_CONTAINER_REGISTRY_URL/$INFRASTRUCTURE_DATA_CONTAINER_NAME:$INFRASTRUCTURE_DATA_CONTAINER_TAG
```





# TODO

1. Sign in to the ODI OpenVolunteering Azure Subscription
1. Create Azure Container Registry: odi-openvolunteering (odi-openvolunteering.azurecr.io)
1. Deploy the fuseki image to it
1. 

Create service principal route? (only useful for CD, probs overkill right now)


# FIRST THING

Export arm template from existing service/resource group

fix up app service data container.



## IDEAS

Make an ontology
Turn on inferencing in Jena
Make ontology serving endpoints (align vocabulary URI)
Serve LODE documentation
Serve WebVOWL
Serve Swagger UI
Serve YasGUI
Finalize mapping
Map ontologies

