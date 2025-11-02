namespace Query;

internal class Endpoint(params IEnumerable<Param> parameters)
{
    internal IEnumerable<Param> Parameters => parameters;
}
