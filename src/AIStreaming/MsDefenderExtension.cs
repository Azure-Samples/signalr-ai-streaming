using System.Net;
using Azure.AI.OpenAI;

public static class MsDefenderExtension
{
    /// <summary>
    /// Checks if Microsoft Defender for Cloud's threat protection for AI workloads enabled.
    /// </summary>
    /// <returns>A bool which represents if Microsoft Defender for Cloud's threat protection for AI workloads enabled</returns>
    public static bool IsMsDefenderForCloudEnabled()
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
        // Currently this sample has no AAD auth implemented for connecting users, in case auth is added, consider passing "EndUserId" and "EndUserTenantId" after extracting it from the user auth token claims
        var userSecurityContext = new UserSecurityContext()
        {
            ApplicationName = Environment.GetEnvironmentVariable("APPLICATION_NAME") ?? String.Empty,
            SourceIP = sourceIp,
        };

        return userSecurityContext;
    }

    private static string GetSourceIp(HttpContext requestContext)
    {
        string? ipAddress = null;
        
        // Check if the request has passed through a proxy or load balancer
        if (requestContext.Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            // The X-Forwarded-For header contains a comma-separated list of IP addresses
            // The first IP address in the list is the original client's IP address
            ipAddress = requestContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        }

        if (string.IsNullOrEmpty(ipAddress))
        {
            // Get the IP address of the client making the request
            ipAddress = requestContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        }

        if (!string.IsNullOrEmpty(ipAddress) && IPAddress.TryParse(ipAddress, out var parsedIpAddress))
        {
            return parsedIpAddress.ToString();
        }

        return ipAddress;
    }
}