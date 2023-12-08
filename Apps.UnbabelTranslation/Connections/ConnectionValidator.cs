using Apps.UnbabelTranslation.Api;
using Apps.UnbabelTranslation.Constants;
using Apps.UnbabelTranslation.Enums;
using Apps.UnbabelTranslation.Models.Entities;
using Apps.UnbabelTranslation.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;

namespace Apps.UnbabelTranslation.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        var creds = authenticationCredentialsProviders.ToArray();
        var customerId = creds.Get(CredsNames.CustomerId).Value;
        var client = new UnbabelTranslationClient(ApiType.Pipelines);


        var endpoint = $"/v0/customers/{customerId}/pipelines";
        var request = new RestRequest(endpoint);

        try
        {
            await client.ExecuteWithErrorHandling<PaginationResponse<TranslationEntity>>(request, creds);

            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}