using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Newtonsoft.Json;
using TestODataExample;
using TestODataExample.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseInMemoryDatabase(databaseName: "AppDb");
});
builder.Services.AddControllers()
    .AddODataNewtonsoftJson()
    .AddOData(options =>
    {
        options.EnableQueryFeatures()
            .AddRouteComponents("odata", GetEdmModel());
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.MapControllers();
app.Run();


IEdmModel GetEdmModel()
{
    var modelBuilder = new ODataConventionModelBuilder();

     modelBuilder.EntitySet<Product>("Products").EntityType.HasKey(product1 => product1.Id);
    
    modelBuilder.EntitySet<Order>("Orders").EntityType.HasKey(product1 => product1.Id);

    return modelBuilder.GetEdmModel();
}