using _360.API.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(cors => cors
   .AddPolicy("MyPolicy", builder =>
   {
      builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
   }));


//Serilog
builder.Host.UseSerilog();

// Application services
builder.Services
    .AddRepositories(builder.Configuration)
    .AddBusinessServices()
    .AddServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache()
   .AddSession(opts =>
   {
      opts.Cookie.IsEssential = true; // make the session cookie Essential
   })
   .AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
   options.SwaggerDoc("v1", new OpenApiInfo
   {
      Version = "v1",
      Title = "_360",
      //Description = "API.",
   });

   options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
   {
      Type = SecuritySchemeType.OAuth2,
      Flows = new OpenApiOAuthFlows
      {

      }
   });

   options.OperationFilter<AuthorizeCheckOperationFilterExtencions>();
});
builder.Services.AddMvcCore();
builder.Services.AddAuthentication(IdentityServer4.AccessTokenValidation.IdentityServerAuthenticationDefaults.AuthenticationScheme)
   .AddIdentityServerAuthentication(options =>
   {
      builder.Configuration.Bind("Authorization", options);
   });
var app = builder.Build();

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
   app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

//app.Run();

#region Logger
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
Log.Logger = new LoggerConfiguration().MinimumLevel
           .Error().MinimumLevel
           .Override("Microsoft", Serilog.Events.LogEventLevel.Error).WriteTo.MSSqlServer(
                   connectionString: builder.Configuration.GetSection("AppSettings:ConnectionString").Value,
                   tableName: builder.Configuration.GetSection("AppSettings:Serilog:TableName").Value,
                   appConfiguration: builder.Configuration,
                   autoCreateSqlTable: true,
                   columnOptionsSection: builder.Configuration.GetSection("AppSettings:Serilog:ColumnOptions"),
                   schemaName: builder.Configuration.GetSection("AppSettings:Serilog:SchemaName").Value
               ).CreateLogger();
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
try
{
   Log.Information("Starting host...");
   app.Run();
   return 0;
}
catch (Exception ex)
{
   Log.Fatal(ex, "Host terminated unexpectedly.");
   return 1;
}
finally
{
   Log.CloseAndFlush();
}
#endregion

