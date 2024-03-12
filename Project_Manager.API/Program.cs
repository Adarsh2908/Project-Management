using Project_Manager.API.Database;
using Project_Manager.API.Services;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Services 
builder.Services.AddSingleton(ServiceProvider =>
{
    var configuration = ServiceProvider.GetService<IConfiguration>();
    var conString = string.Empty;
    if(configuration != null )
    {
        conString = configuration.GetConnectionString("conString") ?? throw new ApplicationException("Connection String is NULL");
    }
    return new SqlConnectionFactory(conString);
});

builder.Services.AddScoped(typeof(UserService));
builder.Services.AddScoped(typeof(AccountService));

var app = builder.Build();

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
