using System.Text.Json;

Console.WriteLine("=== Cliente HTTP Async ===\n");

await DescargarTodoAsync();

Console.WriteLine("\nPrograma terminado.");

// ── MÉTODOS ────────────────────────────────────────

async Task DescargarTodoAsync()
{
    using var client = new HttpClient();
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

    Console.WriteLine("Descargando todo en paralelo...");

    // lanzás las tres tareas SIN await — todavía no esperás
    var taskPosts = client.GetStringAsync("/posts");
    var taskUsers = client.GetStringAsync("/users");
    var taskTodos = client.GetStringAsync("/todos");

    // ahora esperás que las tres terminen juntas
    await Task.WhenAll(taskPosts, taskUsers, taskTodos);

    // cuando llega acá, las tres ya terminaron
    // taskPosts.Result tiene el JSON de posts
    // taskUsers.Result tiene el JSON de users
    // taskTodos.Result tiene el JSON de todos
}

// ── MODELO ─────────────────────────────────────────
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