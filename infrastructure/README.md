# Infrastructure

Azure Resource Manager (ARM) templates define the infrastructure (see contents of this folder).

The [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/) can be used to create the infrastructure.


## Pre-requisite

An [Azure Subscription](https://azure.microsoft.com/en-us/pricing/purchase-options/azure-account).

Login to [azure cli](https://learn.microsoft.com/en-us/cli/azure/) and load infrastructure environment variables.

```zsh
az login
source .env
```


## Azure Resource Group

A group used to manage related Azure resources.

```zsh
az group create --location $INFRASTRUCTURE_LOCATION --name $INFRASTRUCTURE_RESOURCE_GROUP_NAME
```


## Azure Container Registry

A container registry where images can be pushed and automatically deployed via webhooks.

```zsh
az deployment group create --resource-group $INFRASTRUCTURE_RESOURCE_GROUP_NAME --template-file ./infrastructure/azure-container-registry/template.json --parameters name=$INFRASTRUCTURE_CONTAINER_REGISTRY_NAME
```

See: openvolunteering.azurecr.io


## Azure App Service Plan

A set of compute resources for web apps to run.

```zsh
az deployment group create --resource-group $INFRASTRUCTURE_RESOURCE_GROUP_NAME --template-file ./infrastructure/app-service-plan/template.json --parameters name=$INFRASTRUCTURE_APP_SERVICE_PLAN_NAME
```


## Azure App Service: Data Container

A container-web-hosting service.

```zsh
az deployment group create --resource-group $INFRASTRUCTURE_RESOURCE_GROUP_NAME --template-file ./infrastructure/app-service-data-container/template.json --parameters name=$INFRASTRUCTURE_DATA_CONTAINER_APP_SERVICE_NAME app_service_plan_name=$INFRASTRUCTURE_APP_SERVICE_PLAN_NAME
```

NOTE: There seem to be a bug with ARM template of type linux container, the windows kind works (https://github.com/Azure/azure-quickstart-templates/blob/master/quickstarts/microsoft.web/app-service-docs-windows-container/azuredeploy.json) but it is more expensive. TODO: Potentially find a workaround.

Create through Azure UI.


## Azure Container Registry Web Hook

Deploy a new version of the Data Container to the Azure App Service everytime an image with the `latest` tag is published.

Make sure the Continuous Deployment option is activated and SCM basic authentication is enabled (the webhook URL will show if it is) in the App Service's Deployment Center.
