public static class EndpointDefinitionExtensions
{
    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefs = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false } && typeof(IEndpointDefinition).IsAssignableFrom(t))
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var def in endpointDefs)
        {
            def.RegisterEndpoints(app);
        }
    }
}
