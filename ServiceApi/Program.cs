using Service.Api.Generator;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisDistributedCache("RedisCache");

builder.Services.AddScoped<IEmployeeGeneratorService, EmployeeGeneratorService>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.WithOrigins("https://localhost:36905")
          .WithMethods("GET")
          .AllowAnyHeader();
}));

var app = builder.Build();
app.MapDefaultEndpoints();
app.MapGet("/employee", (IEmployeeGeneratorService service, int id) => service.ProcessEmployee(id));
app.UseCors();
app.Run();
