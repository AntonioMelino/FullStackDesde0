using System.Text.Json;

Console.WriteLine("=== Task.WhenAll ===\n");

await DescargarTodoAsync();

Console.WriteLine("\nPrograma terminado.");

// ── MÉTODO PRINCIPAL ───────────────────────────────
async Task DescargarTodoAsync()
{
    try
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

        Console.WriteLine("Lanzando las 3 solicitudes al mismo tiempo...");

        // Las tres se lanzan SIN await — arrancan todas juntas
        var taskPosts = client.GetStringAsync("/posts");
        var taskUsers = client.GetStringAsync("/users");
        var taskTodos = client.GetStringAsync("/todos");

        // Esperamos a que las TRES terminen
        await Task.WhenAll(taskPosts, taskUsers, taskTodos);

        Console.WriteLine("Las 3 solicitudes terminaron.\n");

        // Paso 2 — deserializás cada resultado
        var posts = JsonSerializer.Deserialize<List<Post>>(taskPosts.Result);
        var users = JsonSerializer.Deserialize<List<User>>(taskUsers.Result);
        var todos = JsonSerializer.Deserialize<List<Todo>>(taskTodos.Result);

        // Paso 3 — mostrás cuántos hay de cada uno
        Console.WriteLine($"Posts descargados:  {posts.Count}");
        Console.WriteLine($"Users descargados:  {users.Count}");
        Console.WriteLine($"Todos descargados:  {todos.Count}");

        // Paso 4 — mostrás un resumen de cada colección
        Console.WriteLine("\n--- Primer post ---");
        Console.WriteLine($"[{posts[0].id}] {posts[0].title}");

        Console.WriteLine("\n--- Usuarios ---");
        users.ForEach(u => Console.WriteLine($"  {u.id}. {u.name}"));

        Console.WriteLine("\n--- Todos completados ---");
        todos.Where(t => t.completed)
             .Take(5)
             .ToList()
             .ForEach(t => Console.WriteLine($"  ✓ {t.title}"));
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Error de red: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error inesperado: {ex.Message}");
    }
}

// ── MODELOS ────────────────────────────────────────
public class Post
{
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}

public class User
{
    public int id { get; set; }
    public string name { get; set; }
}

public class Todo
{
    public int id { get; set; }
    public string title { get; set; }
    public bool completed { get; set; }
}