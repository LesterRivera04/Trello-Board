var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var estimaciones = new[]
{
    2, 3, 5, 8, 13, 21
};

app.MapGet("/estimate", () =>
{
    var random = new Random();
    var valor = estimaciones[random.Next(estimaciones.Length)];
    return Results.Ok(valor);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();