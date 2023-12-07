using Apps.UnbabelTranslation.Constants;
using Apps.UnbabelTranslation.Invocables;
using Apps.UnbabelTranslation.Models.Entities;
using Apps.UnbabelTranslation.Models.Request.Translation;
using Apps.UnbabelTranslation.Models.Response.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.UnbabelTranslation.Actions;

[ActionList]
public class TranslationActions : UnbabelTranslationInvocable
{
    public TranslationActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get translation", Description = "Get details of a specific translation")]
    public Task<TranslationEntity> GetTranslation([ActionParameter] TranslationRequest input)
    {
        var endpoint = $"/v1/customers/{CustomerId}/translations/{input.TranslationId}";
        var request = new RestRequest(endpoint);

        return TranslationClient.ExecuteWithErrorHandling<TranslationEntity>(request, Creds);
    }

    [Action("Submit translation", Description = "Submits text for translation")]
    public Task<SubmitTranslationResponse> SubmitTranslation([ActionParameter] SubmitTranslationRequest input)
    {
        var endpoint = $"/v1/customers/{CustomerId}/translations:submit_async";
        var request = new RestRequest(endpoint, Method.Post).WithJsonBody(input, JsonConfig.Settings);

        return TranslationClient.ExecuteWithErrorHandling<SubmitTranslationResponse>(request, Creds);
    }

    [Action("Search translations", Description = "Searches for previously created translations")]
    public async Task<SearchTranslationsResponse> SearchTranslations([ActionParameter] SearchTranslationsRequest input)
    {
        var endpoint = $"/v1/customers/{CustomerId}/translations:search";
        var request = new RestRequest(endpoint, Method.Post);

        var response = await TranslationClient.Paginate<TranslationEntity>(request, Creds,
            JObject.FromObject(input, JsonSerializer.Create(JsonConfig.Settings)));
        return new(response);
    }

    [Action("Cancel translation", Description = "Cancel an ongoing translation")]
    public Task CancelTranslation([ActionParameter] TranslationRequest input)
    {
        var endpoint = $"/v1/customers/{CustomerId}/translations/{input.TranslationId}:cancel";
        var request = new RestRequest(endpoint, Method.Post);

        return TranslationClient.ExecuteWithErrorHandling(request, Creds);
    }
}