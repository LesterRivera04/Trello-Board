using TrelloBoard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// registrar el Service nomal
builder.Services.AddScoped<IUserStoryAPIServices, UserStoryAPIServices>();
builder.Services.AddScoped<IUsuarioAPIServices, UsuarioAPIServices>();

// API principal (CRUD)
builder.Services.AddHttpClient("CrudAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APIBaseUrl"] ?? "https://localhost:7109/api/");
});

// Minimal API
builder.Services.AddHttpClient("MinimalAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5205/");
});

// Minimal API Pokemon
builder.Services.AddHttpClient("APIPokemon", client =>
{
    client.BaseAddress = new Uri("http://localhost:5277/");
    client.Timeout = TimeSpan.FromSeconds(2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Board}/{action=Index}/{id?}");

app.Run();
