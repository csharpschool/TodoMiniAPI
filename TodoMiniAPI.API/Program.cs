var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server Service Registration
builder.Services.AddDbContext<TodoContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("TodosConnection")));

builder.Services.AddCors(policy => {
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
        opt.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
    );
});

ConfigureAutoMapper(builder.Services);
RegisterServices(builder.Services);

var app = builder.Build();

RegisterEndpoints(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Configure CORRS
app.UseCors("CorsAllAccessPolicy");

app.Run();

void ConfigureAutoMapper(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Todo, TodoPostDTO>().ReverseMap();
        cfg.CreateMap<Todo, TodoPutDTO>().ReverseMap();
        cfg.CreateMap<Todo, TodoGetDTO>().ReverseMap();
        cfg.CreateMap<Status, StatusPostDTO>().ReverseMap();
        cfg.CreateMap<Status, StatusPutDTO>().ReverseMap();
        cfg.CreateMap<Status, StatusGetDTO>().MaxDepth(2).ReverseMap();
        cfg.CreateMap<Tag, TagPostDTO>().ReverseMap();
        cfg.CreateMap<Tag, TagPutDTO>().ReverseMap();
        cfg.CreateMap<Tag, TagGetDTO>().ReverseMap();
        cfg.CreateMap<TodoTag, TodoTagPostDTO>().ReverseMap();
        cfg.CreateMap<TodoTag, TodoTagPutDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    services.AddSingleton(mapper);
}

void RegisterServices(IServiceCollection services)
{
    services.AddScoped<DbService>();
    services.AddTransient<IEndpoint, StatusEndpoint>();
    services.AddTransient<IEndpoint, TagEndpoint>();
    services.AddTransient<IEndpoint, TodoEndpoint>();
    services.AddTransient<IEndpoint, TodoTagEndpoint>();
}

void RegisterEndpoints(WebApplication app)
{
    var endpoints = app.Services.GetServices<IEndpoint>();

    foreach (var endpoint in endpoints)
    {
        if (endpoint is null) throw new InvalidProgramException("Couldn't register API.");

        endpoint.Register(app);
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
