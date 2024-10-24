using Api_Innovatech.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de la cadena de conexi�n
var Configuracion = new Configuracion(builder.Configuration.GetConnectionString("innovatech_500"));
builder.Services.AddSingleton(Configuracion);

// Registro de servicios
builder.Services.AddScoped<IProducto, CrudProducto>();
builder.Services.AddScoped<IProveedor, CrudProveedor>();
builder.Services.AddScoped<IUsuario, CrudUsuario>();

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar la pol�tica de CORS
app.UseCors("PermitirTodo");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
