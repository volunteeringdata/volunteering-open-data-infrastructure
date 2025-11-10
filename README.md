# Open Volunteering Infrastructure


## Data Transformation

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

See: openvolunteering.azurecr.io

### Azure App Service Plan

A set of compute resources for web apps to run.

```zsh
az deployment group create --resource-group $INFRASTRUCTURE_RESOURCE_GROUP_NAME --template-file ./infrastructure/app-service-plan/template.json --parameters name=$INFRASTRUCTURE_APP_SERVICE_PLAN_NAME
```

### Azure App Service: Data Container

A container-web-hosting service.

```zsh
az deployment group create --resource-group $INFRASTRUCTURE_RESOURCE_GROUP_NAME --template-file ./infrastructure/app-service-data-container/template.json --parameters name=$INFRASTRUCTURE_DATA_CONTAINER_APP_SERVICE_NAME app_service_plan_name=$INFRASTRUCTURE_APP_SERVICE_PLAN_NAME
```

NOTE: There seem to be a bug with ARM template of type linux container, the windows kind works (https://github.com/Azure/azure-quickstart-templates/blob/master/quickstarts/microsoft.web/app-service-docs-windows-container/azuredeploy.json) but it is more expensive. TODO: Potentially find a workaround.

Create through Azure UI.

### Azure Container Registry Web Hook

Deploy a new version of the Data Container to the Azure App Service everytime an image with the `latest` tag is published.

Make sure the Continuous Deployment option is activated and SCM basic authentication is enabled (the webhook URL will show if it is) in the App Service's Deployment Center


## Data Container

A readonly triplestore with a sparql endpoint containing the latest data (including `./fuseki/data.ttl` & `./fuseki/vocabulary.ttl`).

### Build Container

```zsh
docker build --build-arg JENA_VERSION=5.6.0 --tag $INFRASTRUCTURE_DATA_CONTAINER ./fuseki
```

### Deploy Container

```zsh
az login
az acr login --name $INFRASTRUCTURE_CONTAINER_REGISTRY_NAME
docker push $INFRASTRUCTURE_DATA_CONTAINER
```

### Run Container

SPARQL endpoint accessible at http://localhost:3030/sparql.

```zsh
docker run --rm -p 3030:3030 $INFRASTRUCTURE_DATA_CONTAINER
```

Example Query (URI encode the query and use http://localhost:3030/sparql?query=):
- Get all activities `CONSTRUCT { ?s <https://id.example.org/schema/activityTitle> ?o } WHERE { ?s <https://id.example.org/schema/activityTitle> ?o }`
- Get all statements `CONSTRUCT { ?s ?p ?o } WHERE { ?s ?p ?o }`
- Get the vocabulary `CONSTRUCT { ?s ?p ?o . } WHERE { ?s ?p ?o ; <http://www.w3.org/2000/01/rdf-schema#isDefinedBy> <https://id.example.org/schema> . }`






# TODO

Try smaller or more optimized formats for RDF loading.
Create service principal route? (only useful for CD, probs overkill right now).
Export full arm template from Resource Group.


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

