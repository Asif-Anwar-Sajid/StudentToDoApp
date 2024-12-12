using StudentToDoApi.Services;
using StudentToDoApi.Data; // Make sure you include this for MongoContext
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB connection
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection") ?? "mongodb://localhost:27017";  // Default to local MongoDB
var databaseName = builder.Configuration["MongoDbDatabaseName"] ?? "StudentToDoAppDb";  // Default to "StudentToDAppoDb"

// Register MongoContext as a service (dependency injection)
builder.Services.AddSingleton(new MongoContext(mongoConnectionString, databaseName));

// Register services for dependency injection
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<ToDoService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Set up the request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
