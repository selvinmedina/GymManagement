using GymManagement.Api;
using GymManagement.Application;
using GymManagement.Infrastructure;
var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddPresentation();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler();
    app.AddInfrastructureMiddleware();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}