using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductFlow.Infrastructure.DataAccess;

namespace ProductFlow.Infrastructure.Migrations;

public static class DataBaseMigration
{
    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<ProductFlowDbContext>();

        await dbContext.Database.MigrateAsync();
    } 
}