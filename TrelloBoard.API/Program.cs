using TrelloBoard.API.Data;
using TrelloBoard.API.Repository;
using TrelloBoard.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//agregar el DbContext para hacerlo EF a lo Lester
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped<IUserStoryRepository, UserStoryRepository>();
builder.Services.AddScoped<IUserStoryServices, UserStoryServices>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();


builder.Services.AddHttpClient<UserStoryServices>(); // con esto puedo llamar al Minimal API desde el servicio de UserStoryServices


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
