# Volunteering Open Data Infrastructure

This repository holds code for modeling and accessing open data for volunteering.


## Data Model

See the [volunteering data model repository](https://github.com/volunteeringdata/data-model).


## API

1. [OpenAPI description document](https://query20251112104247-h6affebdd4gfa4bs.uksouth-01.azurewebsites.net/openapi.json)
2. [Swagger UI](https://query20251112104247-h6affebdd4gfa4bs.uksouth-01.azurewebsites.net/swagger)


## How To

### Set up and understand the infrastructure

The infrastructure consists of a Container Registry and two app services: 1 for the API and 1 for the database.

Some infrastructure as code (IaC) in the form of ARM templates is here to help out.

See the [infrastructure README](`./infrastructure/README.md`) for details.


### Transform Data

#### Run automated data transformation for DoIt Data sample

```zsh
dotnet run ./data/doit/activities.json ./data/doit/data.ttl --project ./transform/DoIt/
```


### Publish Data

You need to login, build the data container and publish it. A WebHook automatically deploys the latest data container version available to the infrastructure.

The data container is a readonly triplestore with a public sparql endpoint containing the latest data (including this repos' `.ttl` files).

#### Requirements

```zsh
source .env
az login
az acr login --name $INFRASTRUCTURE_CONTAINER_REGISTRY_NAME
```

#### Execution

```zsh
docker build --tag $INFRASTRUCTURE_DATA_CONTAINER .
docker push $INFRASTRUCTURE_DATA_CONTAINER
```


### Run the Data Container Locally

```zsh
docker run --rm -p 3030:3030 $INFRASTRUCTURE_DATA_CONTAINER
```

SPARQL endpoint accessible at http://localhost:3030/sparql.

Example Query (URI encode the query and use it as parameter to http://localhost:3030/sparql?query=):
- Get all activities `CONSTRUCT { ?s <https://id.example.org/schema/activityTitle> ?o } WHERE { ?s <https://id.example.org/schema/activityTitle> ?o }`
- Get all statements `CONSTRUCT { ?s ?p ?o } WHERE { ?s ?p ?o }`
- Get the vocabulary `CONSTRUCT { ?s ?p ?o . } WHERE { ?s ?p ?o ; <http://www.w3.org/2000/01/rdf-schema#isDefinedBy> <https://id.example.org/schema> . }`


### Run the API Locally

#### Requirements

Manage user secrets (right click Query.csproj > Manage User Secrets) and set the SparqlEndpointUri.

The `secrets.json` file should look like this:

```json
{
  "QueryService": {
    "SparqlEndpointUri": "https://openvolunteeringdata-edd0h6d2dwcaa8br.uksouth-01.azurewebsites.net/sparql"
  }
}
```

#### Execution

```zsh
dotnet run --project Query
```

Go to: http://localhost:5199/swagger


# TODO


## Tooling

Validator service:
- Based on a SHACL document
- Give it a URI for the SHACL doc
- Give it a URI for the data

Publish a JSON-LD context (so that all you get is @context: URI and a JSON format).

Give a proper JSON format for the main classes.

Do a very embedded format and one more distributed.

(flat vs nested structure and json ld doesn't care)

CSV format.

## Others

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


## TODO NOW

Improve Vocabulary
GeoSPARQL Query
Parameterised Query
Swagger UI
Fix up 


http://localhost:5199/geo_example2.html?lat=51.36&lon=-0.37&within=50




### Notes

Only required properties for an activity are: organisation, label and description.
TODO: Confirm that activities MUST have an organisation.

If an activity has no Location, it is remote?
    An activity with no location might be remote but it is possible that location information is held in the activity's description.

How to know with certainty if an activity is remote?
    An activity accomodates remote participation if the "allows remote participation" property is set to true.



one activity can have multiple location so does each location have a different contact, location can be a wide area (neighbourhood).
What if an activity has different locations depending on the session?
    Then it is a different activity.

Taxonomy must be refined. Concept should have classes: Skills/Requirements, Charitable Aims/Cause, Volunteer Rewards (training, networking, experience, cost reimbursement), Accessibilty, Language, Demographic Targetting.

Organisation must have size.

Contact has Activity, Organisation and Location.

Systems API definitions should be discoverable (do we need a property to link to the API definition).

Time must accomodate repeat (every monday...), there are volunteering roles without clear time constraints and others that are session-based.

Contact is misleading maybe, it seems people think of it as a person rather than as a contact information/line of communication.


### Questions:
1. License
2. Procure a Domain (openvolunteering.org or volunteeringdata.io)


We could model it so that an activity has sessions:
with specific times and place and contact

