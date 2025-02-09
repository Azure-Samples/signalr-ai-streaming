using Azure.AI.OpenAI;

public static class MsDefenderExtension
{
    /// <summary>
    /// Checks if Microsoft Defender for AI is enabled.
    /// </summary>
    /// <returns>A bool which represents if Microsoft Defender for AI is enabled</returns>
    public static bool IsMsDefenderForAIEnabled()
    {
        var defenderForCloudEnabled = Environment.GetEnvironmentVariable("MS_DEFENDERFORCLOUD_ENABLED");
        return !string.IsNullOrEmpty(defenderForCloudEnabled) && defenderForCloudEnabled.Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Generates the user security context which contains several parameters that describe the AI application itself, and the end user that interacts with the AI application.
    /// These fields assist your security operations teams to investigate and mitigate security incidents by providing a comprehensive approach to protecting your AI applications.
    /// <see href="https://learn.microsoft.com/en-us/azure/defender-for-cloud/gain-end-user-context-ai">Learn more</see> about protecting AI applications using Microsoft Defender for Cloud.
    /// </summary>
    /// <param name="request">The HTTP context</param>
    /// <returns>UserSecurityContext which represents the user context</returns>
    public static UserSecurityContext GetUserSecurityContext(HttpContext requestContext)
    {
        var sourceIp = GetSourceIp(requestContext);
        // Currently this sample has no AAD auth implemented for commecting users, in case auth is added, consider passing "EndUserId" and "EndUserTenantId" after extracting it from the user auth token claims
        var userObject = new UserSecurityContext()
        {
            // If authentication is enabled, consider to add the keys: "EndUserTenantId", "EndUserId"
            ApplicationName = Environment.GetEnvironmentVariable("APPLICATION_NAME") ?? String.Empty,
            SourceIP = sourceIp,
        };

        return userObject;
    }

    private static string GetSourceIp(HttpContext? requestContext)
    {
        if (requestContext == null)
        {
            return string.Empty;
        }

        if (!requestContext.Request.Headers.TryGetValue("X-Forwarded-For", out var xForwardForHeaders))
        {
            return requestContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
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