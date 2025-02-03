//using Microsoft.Azure.Functions.Worker.Http;

public static class MsDefenderExtension
{
    /// <summary>
    /// Checks if Microsoft Defender for AI is enabled.
    /// </summary>
    /// <returns>A json bool which represents if Microsoft Defender for AI is enabled</returns>
    public static bool IsMsDefenderForAIEnabled()
    {
        var defenderForCloudEnabled = Environment.GetEnvironmentVariable("MS_DEFENDERFORCLOUD_ENABLED");
        Console.WriteLine($"MS_DEFENDERFORCLOUD_ENABLED: {defenderForCloudEnabled}");
        return !string.IsNullOrEmpty(defenderForCloudEnabled) && defenderForCloudEnabled.Equals("true", StringComparison.OrdinalIgnoreCase);;
    }

    /// <summary>
    /// Generates the user security context which contains several parameters that describe the AI application itself, and the end user that interacts with the AI application.
    /// These fields assist your security operations teams to investigate and mitigate security incidents by providing a comprehensive approach to protecting your AI applications.
    /// <see href="https://learn.microsoft.com/en-us/azure/defender-for-cloud/gain-end-user-context-ai">Learn more</see> about protecting AI applications using Microsoft Defender for Cloud.
    /// </summary>
    /// <param name="request">The HTTP request</param>
    /// <returns>A json string which represents the user context</returns>
    public static string GetMsDefenderUserJson(HttpContext? requestContext)
    {
        var sourceIp = GetSourceIp(requestContext);
        // If authentication is enabled, consider to add the keys: "EndUserTenantId", "EndUserId", "EndUserIdType"
        var userObject = new Dictionary<string, string>
        {
             { "SourceIp", sourceIp },
             { "ApplicationName", Environment.GetEnvironmentVariable("APPLICATION_NAME") ?? String.Empty }, // Value should be the name of your application
        };

        Console.WriteLine($"APPLICATION_NAME: {Environment.GetEnvironmentVariable("APPLICATION_NAME")}");
        return System.Text.Json.JsonSerializer.Serialize(userObject);
    }

    private static string GetSourceIp(HttpContext? requestContext)
    {
        // If your application is NOT behind a proxy or load balancer, this function should return:
        // httpContext.Connection.RemoteIpAddress?.ToString();

        if (requestContext == null || !requestContext.Request.Headers.TryGetValue("X-Forwarded-For", out var xForwardForHeaders))
        {
            return string.Empty;
        }

        Console.WriteLine($"X-Forwarded-For: {xForwardForHeaders.FirstOrDefault()}");
        Console.WriteLine($"By RemoteIpAddress: {requestContext.Connection.RemoteIpAddress?.ToString()}");

        var ip = xForwardForHeaders.FirstOrDefault()?.Split(',')[0];
        if (ip == null)
        {
            return string.Empty;
        }

        var colonIndex = ip.LastIndexOf(':');

        // case of ipv4
        if (colonIndex != -1 && ip.IndexOf(':') == colonIndex)
        {
            return ip.Substring(0, colonIndex);
        }

        // case of ipv6
        if (ip.StartsWith('[') && ip.Contains("]:"))
        {
            return ip.Substring(0, ip.IndexOf("]:") + 1);
        }

        return ip;
    }
}