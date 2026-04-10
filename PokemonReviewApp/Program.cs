using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Step 1: Add initial Data into the DB that add data from Seed.cs
builder.Services.AddTransient<Seed>();

builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

// Dependency Injection between interfaces, Repository
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

// Connected Database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Step 2: Add initial Data into the DB that add data from Seed.cs
if (args.Length == 1 && args[0].ToLower() == "seedData")
    SeedData(app);

void SeedData(IHost App)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        //var service = scope.ServiceProvider.GetService<Seed>();
        var service = scope.ServiceProvider.GetRequiredService<Seed>();
        service.SeedDataContext();
    }
}
// end of data add 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
