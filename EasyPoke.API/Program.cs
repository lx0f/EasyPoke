using EasyPoke.API.Config;
using EasyPoke.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;
Configure.ConfigureServices(services, configuration);

var app = builder.Build();

// Build Pokemon Data
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<DataContext>())
    Configure.ConfigurePokemonData(context);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
