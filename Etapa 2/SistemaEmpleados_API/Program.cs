using Microsoft.EntityFrameworkCore;
using SistemaEmpleados_API.Data;
using SistemaEmpleados_API.Interfaces;
using SistemaEmpleados_API.Services;

var builder = WebApplication.CreateBuilder(args);

// ?? SERVICIOS ??????????????????????????????????????

// Base de datos
builder.Services.AddDbContext<EmpresaContext>(options =>
    options.UseSqlServer(
        @"Server=(localdb)\MSSQLLocalDB;Database=SistemaEmpleadosAPI;Trusted_Connection=True;"
    ));

// Inyección de dependencias — conecta la interfaz con la implementación
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

// Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ?? CREAR BD SI NO EXISTE ??????????????????????????
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EmpresaContext>();
    await db.Database.EnsureCreatedAsync();
}

// ?? MIDDLEWARE ?????????????????????????????????????
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();