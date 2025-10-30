# X

## Graph Backend

### Jena

#### Fuseki

A containe

#### Jena TDB2 Loader



## Run the Graph Database backend

```zsh
cd docker
docker compose build --build-arg JENA_VERSION=5.6.0
docker compose run --rm --service-ports fuseki --mem /ds


docker compose run --rm --service-ports fuseki --mem /ds


--file FILE /name

docker compose run --rm --name MyServer --service-ports fuseki --tdb2 --loc databases/DB2 /ds --file /fuseki/databases/x.ttl

```

http://localhost:3030/ds/sparql?query=CONSTRUCT%20{%20?a%20?b%20?c%20}%20WHERE%20{%20?a%20?b%20?c%20}




## Load data from file
#RUN tdb2.tdbloader --loc databases/DB2 MyData.ttl
# exec "$JAVA_HOME/bin/java" $JAVA_OPTIONS -jar "/tdb/jena-tdb2-5.6.0.jar" --help

#RUN $JAVA_HOME/bin/java $JAVA_OPTIONS -cp "${JENA_DIR}/${JENA_TAR}" org.apache.jena.tdb2.TDB2 

# https://downloads.apache.org/jena/binaries/

# https://dlcdn.apache.org/jena/binaries/apache-jena-5.6.0.zip

