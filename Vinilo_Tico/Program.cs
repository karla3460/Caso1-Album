using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using DA;
using Flujo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Inyección de dependencias (capas del proyecto)
builder.Services.AddScoped<IAlbumFlujo, AlbumFlujo>();
builder.Services.AddScoped<IAlbumDA, AlbumDA>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Vinilo Tico API");
    });
}

app.UseHttpsRedirection();

app.UseDefaultFiles(); // primero: busca index.html en la raíz
app.UseStaticFiles();  // después: sirve los archivos de wwwroot

app.UseAuthorization();

app.MapControllers();

app.Run();