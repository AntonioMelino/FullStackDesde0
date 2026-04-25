using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Etapa1.Data;
using ProyectoFinal_Etapa1.Services;
using ProyectoFinal_Etapa1.UI;

// Crear contexto y asegurar BD
var db = new EmpresaContext();
await db.Database.EnsureCreatedAsync();

// Conectar capas
var service = new EmpleadoService(db);
var menu = new Menu(service);

// Arrancar
await menu.EjecutarAsync();