using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Respawn;
using Testcontainers.MySql;

namespace PaymentGateway.Tests.Integration.Shared;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer _mySqlContainer;
    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;
    public HttpClient AnonymousClient { get; private set; } = default!;


    public IntegrationTestFactory()
    {
        _mySqlContainer = new MySqlBuilder()
            .WithImage("mysql:8.3")
            .WithDatabase("class_task")
            .WithCleanUp(true)
            .WithAutoRemove(true)
            .WithName("class_task_integration_test")
            .Build();


    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        Configuration = GetTestConfig();
        builder.UseConfiguration(Configuration);
    }

    private IConfiguration GetTestConfig()
    {
        var dbConfig = new Dictionary<string, string?>
        {
            ["ConnectionStrings:DevString"] =
           $"server={_mySqlContainer.Hostname};" +
           $"port={_mySqlContainer.GetMappedPublicPort(MySqlBuilder.MySqlPort)};" +
           $"database=class_task;" +
           $"user={MySqlBuilder.DefaultUsername};" +
           $"password={MySqlBuilder.DefaultPassword};"
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(dbConfig) // This overrides JSON with container-based values
            .AddEnvironmentVariables()
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _mySqlContainer.StartAsync();
        InitiateClients();
        await InitializeRespawner();
        await ResetDatabase();
    }

    private async Task InitializeRespawner()
    {
        _dbConnection = new SqlConnection(_mySqlContainer.GetConnectionString());
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection,
            new() { DbAdapter = DbAdapter.Postgres, SchemasToInclude = ["public"] });
    }




    public async Task ResetDatabase()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    public new async Task DisposeAsync()
    {
        await _mySqlContainer.DisposeAsync();
    }

    private void InitiateClients()
    {
        AnonymousClient = CreateClient();
    }


    public IConfiguration Configuration { get; private set; } = default!;
}
