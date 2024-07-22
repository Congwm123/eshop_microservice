﻿using Microsoft.EntityFrameworkCore;

namespace DisCount.Grpc.Data;

public static class Extensions
{
    public static IApplicationBuilder UserMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbContext.Database.MigrateAsync();
        return app;
    }
}
