using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Extensions.Options;
using OpenAI;

namespace AIStreaming
{
    public static class OpenAIExtensions
    {
        public static IServiceCollection AddAzureOpenAI(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<OpenAIOptions>(configuration.GetSection("OpenAI"))
                .AddSingleton<OpenAIClient>(provider =>
                {
                    var options = provider.GetRequiredService<IOptions<OpenAIOptions>>().Value;
                    return new AzureOpenAIClient(new Uri(options.Endpoint), new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = options.IdentityClientId }));
                });
        }
    }
}
