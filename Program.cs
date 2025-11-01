var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// API endpoint que consulta OpenWeatherMap (la API key se toma de la variable de entorno OPENWEATHER_API_KEY)
app.MapGet("/api/weather", async (IHttpClientFactory httpFactory, double? lat, double? lon, int? cnt) =>
{
    var apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");
    if (string.IsNullOrWhiteSpace(apiKey))
    {
        return Results.Problem("OPENWEATHER_API_KEY no configurada en variables de entorno", statusCode: 500);
    }

    // Coordenadas por defecto: Medellín
    const double defaultLat = 6.2442;
    const double defaultLon = -75.5812;

    var qLat = lat ?? defaultLat;
    var qLon = lon ?? defaultLon;
    // Clamp lat/lon to valid ranges
    qLat = Math.Clamp(qLat, -90.0, 90.0);
    qLon = Math.Clamp(qLon, -180.0, 180.0);

    var count = cnt ?? 8;
    count = Math.Clamp(count, 1, 50);

    var client = httpFactory.CreateClient();
    var url = $"https://api.openweathermap.org/data/2.5/find?lat={qLat}&lon={qLon}&cnt={count}&units=metric&lang=es&appid={apiKey}";
    var res = await client.GetAsync(url);

    if (!res.IsSuccessStatusCode)
        return Results.Problem($"Error al obtener datos del clima: {res.StatusCode}", statusCode: 502);

    var json = await res.Content.ReadAsStringAsync();
    return Results.Content(json, "application/json");
});

app.MapRazorPages();

app.Run();
