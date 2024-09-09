namespace AIStreaming
{
    public class OpenAIOptions
    {
        /// <summary>
        /// The endpoint of Azure OpenAI service. Only available for Azure OpenAI.
        /// </summary>
        public string? Endpoint { get; set; }

        /// <summary>
        /// Client ID for the managed identity.
        /// </summary>
        public string? IdentityClientId { get; set; }

        /// <summary>
        /// The model to use.
        /// </summary>
        public string? Model { get; set; }
    }
}
