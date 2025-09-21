namespace Environment;

public static class EnvironmentExtensions
{
    private const string PostgreSqlConnectionstring = "POSTGRESQL_CONNECTIONSTRING";
    
    public static string GetPostgresConnectionString() => System.Environment.GetEnvironmentVariable(PostgreSqlConnectionstring);
}