#pragma warning disable IDE0005
using System.Text.Json;
using System.Text.Json.Serialization;
using VerifyTests;
using VerifyXunit;


namespace PaymentGateway.Tests.Integration.Shared;

[Collection("IntegrationTests")]
public class IntegrationTestBase : VerifyBase, IAsyncLifetime
{
    private readonly IntegrationTestFactory _applicationFactory;
    private readonly Func<Task> _resetDatabase;

    protected readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = false
    };

    public HttpClient AnonymousClient { get; private set; } = default!;

   

    public IntegrationTestBase(IntegrationTestFactory applicationFactory) : base()
    {
        _applicationFactory = applicationFactory;
        InitiateClients();
        _resetDatabase = applicationFactory.ResetDatabase;
    }

    private void InitiateClients()
    {
        AnonymousClient = _applicationFactory.AnonymousClient;
    }

    public Task InitializeAsync()
    {
        return _resetDatabase();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    protected VerifySettings GetVerifySettings()
    {
        var classname = GetType().Name;
        var settings = new VerifySettings();
        settings.UseDirectory($"Results/{classname}");
        settings.ScrubInlineGuids();
        return settings;
    }
}