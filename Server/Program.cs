using Carter;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation;
using Infrastructure.Authentication;
using Server.JwtOptionsSetup;


var builder = WebApplication.CreateBuilder(args);

builder.Services.
    AddApplication().
    AddPresentation().
    AddInfrastructure(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(i =>
    {
        i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting();
builder.Services.AddCarter();

builder.Services.AddSwaggerGen(options =>
{
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();


app.UseAuthentication();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();
app.UseAuthentication();

app.MapControllers();

app.Run();
