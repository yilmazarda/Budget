var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
