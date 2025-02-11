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
        // Currently this sample has no AAD auth implemented for commecting users, in case auth is added, consider passing "EndUserId" and "EndUserTenantId" after extracting it from the user auth token claims
        var userObject = new UserSecurityContext()
        {
            // If authentication is enabled, consider to add the keys: "EndUserTenantId", "EndUserId"
            ApplicationName = Environment.GetEnvironmentVariable("APPLICATION_NAME") ?? String.Empty,
            SourceIP = sourceIp,
        };

        return userObject;
    }

    private static string GetSourceIp(HttpContext requestContext)
    {
        var remoteIp = GetClientIpAddress(requestContext);
        var ip = remoteIp.Split(',')[0];
        if (!string.IsNullOrEmpty(ip) && IPAddress.TryParse(ip, out var ipAddress))
        {
            return ipAddress.ToString();
        }

        return ip;
    }

    private static string GetClientIpAddress(HttpContext requestContext)
    {
        // You can add more proxy headers to check for the IP address
        if (requestContext.Request.Headers.TryGetValue("X-Forwarded-For", out var xForwardForHeaders))
        {
            return xForwardForHeaders.FirstOrDefault() ?? GetRemoteIpFromConnection(requestContext);
        }
        else
        {
            return GetRemoteIpFromConnection(requestContext);
        }
    }

    private static string GetRemoteIpFromConnection(HttpContext requestContext)
    {
        return requestContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }
}