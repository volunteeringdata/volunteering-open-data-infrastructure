# x

## Transformation

The following creates RDF from JSON:

```zsh
dotnet run ./data/doit/activities.json ./fuseki/data.ttl --project ./transform/DoIt/
```

## Graph Backend

The Dockerfile loads `./fuseki/data.ttl` and runs a readonly fuseki instance.

SPARQL endpoint accessible at http://localhost:3030/sparql.

```zsh
cd fuseki
docker compose build --build-arg JENA_VERSION=5.6.0
docker compose run --rm --service-ports fuseki
```

Example Query: `http://localhost:3030/sparql?query=CONSTRUCT { ?s ?p ?o } WHERE { ?s ?p ?o }`.
