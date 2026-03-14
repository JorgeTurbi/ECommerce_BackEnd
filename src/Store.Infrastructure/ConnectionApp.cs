namespace Store.Infrastructure;


public class ConnectionApp
{
    public string Server { get; }
    public string User { get; }
    public string Password { get; }
    public string Database { get; set; }

    public ConnectionApp()
    {
        Server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "Dev001";

        User = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";

        Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Brittany040238.";

        Database = Environment.GetEnvironmentVariable("DB_DataBase") ?? "StoreDb";

    }
}
