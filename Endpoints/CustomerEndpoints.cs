using libretapp.Data;
using Microsoft.EntityFrameworkCore;

public class CustomerEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var api = app.MapGroup("/api/customers");

        api.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Customers.ToListAsync();
        });
    }
}