namespace Environment;

public static class EnvironmentExtensions
{
    private const string PostgreSqlDataConnectionstring = "POSTGRESQL_CONNECTIONSTRING";
    
    private const string PostgreSqlIdentityConnectionstring = "POSTGRESQL_IDENTITY_CONNECTIONSTRING";

    private const string PlaywrightServerConnectionString = "PLAYWRIGHT_SERVER";
    
    
    public static string GetPostgresConnectionString() => System.Environment.GetEnvironmentVariable(PostgreSqlDataConnectionstring);
    
    public static string GetPlaywrightServer()  => System.Environment.GetEnvironmentVariable(PlaywrightServerConnectionString);
    
    public static string GetIdentityPostgresConnectionString() => System.Environment.GetEnvironmentVariable(PostgreSqlIdentityConnectionstring);
    
    public static string GetJwtSigningKey()  => System.Environment.GetEnvironmentVariable("JWT_SIGNING_KEY");
    
    public static string GetGooglePlacesApiKey() => System.Environment.GetEnvironmentVariable("GOOGLE_PlACES_API_KEY");
}