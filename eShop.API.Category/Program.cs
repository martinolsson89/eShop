using eShop.API.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adds services to the DI container. This is crucial for configuring the app's services and middleware.
builder.Services.AddEndpointsApiExplorer(); // Enables API explorer, helpful for Swagger.
builder.Services.AddSwaggerGen(); // Adds Swagger generator services.

// Adds Entity Framework services to the DI container and configures 'UseSqlServer' as the database provider.
builder.Services.AddDbContext<EShopContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("ElectronicShopConnection")));

//CORS (Cross Origin Resource Sharing)
// Configures CORS policy to allow any origin, header, and method, which is useful for cross-origin requests.
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
        opt.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

RegisterServices();

var app = builder.Build(); // Builds the WebApplication instance.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS.

RegisterEndpoints(); // Custom method to register API endpoints.

app.UseCors("CorsAllAccessPolicy"); // Applies the CORS policy configured above.

app.Run(); // Runs the application.


// Custom method to register services. Scoped services are created once per client request.
void RegisterServices()
{
    ConfigureAutoMapper();
    builder.Services.AddScoped<IDbService, CategoryDbService>();
}
void RegisterEndpoints()
{
    app.AddEndpoint<Category, CategoryPostDTO, CategoryPutDTO, CategoryGetDTO>();

    //Should be placed in separate API project
    //app.AddEndpoint<ProductCategory, ProductCategoryDTO>();

}
void ConfigureAutoMapper()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Category, CategoryPostDTO>().ReverseMap();
        cfg.CreateMap<Category, CategoryGetDTO>().ReverseMap();
        cfg.CreateMap<Category, CategoryPutDTO>().ReverseMap();
        cfg.CreateMap<Category, CategorySmallGetDTO>().ReverseMap();

        cfg.CreateMap<Filter, FilterGetDTO>().ReverseMap();
        cfg.CreateMap<Color, OptionDTO>().ReverseMap();
        cfg.CreateMap<Fuel, OptionDTO>().ReverseMap();
        cfg.CreateMap<Brand, OptionDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}
