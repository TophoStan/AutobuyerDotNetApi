namespace Environment;

public static class EnvironmentExtensions
{
    private const string PostgreSqlConnectionstring = "POSTGRESQL_CONNECTIONSTRING";

    private const string PlaywrightServerConnectionString = "PLAYWRIGHT_SERVER";
    
    public static string GetPostgresConnectionString() => System.Environment.GetEnvironmentVariable(PostgreSqlConnectionstring);
    
    public static string GetPlaywrightServer()  => System.Environment.GetEnvironmentVariable(PlaywrightServerConnectionString);
}