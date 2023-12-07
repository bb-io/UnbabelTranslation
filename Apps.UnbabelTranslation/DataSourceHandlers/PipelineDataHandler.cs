using Apps.UnbabelTranslation.Invocables;
using Apps.UnbabelTranslation.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.UnbabelTranslation.DataSourceHandlers;

public class PipelineDataHandler : UnbabelTranslationInvocable, IAsyncDataSourceHandler
{
    public PipelineDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var endpoint = $"/v0/customers/{CustomerId}/pipelines";
        var request = new RestRequest(endpoint);

        var response = await PipelineClient.Paginate<PipelineEntity>(request, Creds);

        return response
            .Where(x => context.SearchString is null ||
                        x.DisplayName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id, x => x.DisplayName);
    }
}