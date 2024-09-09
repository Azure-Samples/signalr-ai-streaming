using AIStreaming;
using AIStreaming.Hubs;
using Azure.Identity;
using Microsoft.Azure.SignalR;

var builder = WebApplication.CreateBuilder(args);

var endpoint = builder.Configuration.GetValue<string>("Azure:SignalR:Endpoint");
var clientId = builder.Configuration.GetValue<string>("Azure:SignalR:IdentityClientId");

// Add services to the container.
builder.Services.AddSignalR().AddAzureSignalR(option =>
{
    option.Endpoints = [
        new ServiceEndpoint(new Uri(endpoint), new ManagedIdentityCredential(clientId)),
    ];
});
builder.Services.AddSingleton<GroupAccessor>()
    .AddSingleton<GroupHistoryStore>()
    .AddAzureOpenAI(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapHub<GroupChatHub>("/groupChat");
app.Run();
