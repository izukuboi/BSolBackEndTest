﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackFinance.Infrastructure.Data;

namespace TrackFinance.Infrastructure;
public static class StartupSetup
{
  public static void AddDbContext(this IServiceCollection services, string connectionString) =>
      services.AddDbContext<AppDbContext>(options =>
          //options.UseSqlite(connectionString)); // will be created in web project root
          options.UseSqlServer(connectionString)); // sqlserver > sqlite
}
