using Apps.UnbabelTranslation.Constants;
using Apps.UnbabelTranslation.Enums;
using Apps.UnbabelTranslation.Extensions;
using Apps.UnbabelTranslation.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.UnbabelTranslation.Api;

public class UnbabelTranslationClient : BlackBirdRestClient
{
    protected override JsonSerializerSettings? JsonSettings => JsonConfig.Settings;

    public UnbabelTranslationClient(ApiType apiType = ApiType.Translations) : base(new()
    {
        BaseUrl = apiType switch
        {
            ApiType.Translations => Urls.TranslationApi.ToUri(),
            ApiType.Pipelines => Urls.PipelinesApi.ToUri(),
            _ => throw new ArgumentOutOfRangeException(nameof(apiType), apiType, null)
        }
    })
    {
    }

    public async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request,
        AuthenticationCredentialsProvider[] creds)
    {
        var token = await creds.GetAccessToken();
        request.AddHeader("Authorization", $"Bearer {token.AccessToken}");

        return await ExecuteWithErrorHandling(request);
    }

    public async Task<T> ExecuteWithErrorHandling<T>(RestRequest request, AuthenticationCredentialsProvider[] creds)
    {
        var token = await creds.GetAccessToken();
        request.AddHeader("Authorization", $"Bearer {token.AccessToken}");

        return await ExecuteWithErrorHandling<T>(request);
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (response.Content is null)
            return new("Something went wrong");
        
        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content, JsonSettings)!;

        if (errorResponse.Message is not null)
            return new(errorResponse.Message);

        if (errorResponse.ErrorDescription is not null)
            return new(errorResponse.ErrorDescription);

        if (errorResponse.Detail is not null)
            return new(errorResponse.Detail);

        return new("Something went wrong");
    }

    public async Task<List<T>> Paginate<T>(RestRequest request, AuthenticationCredentialsProvider[] creds,
        JObject payload)
    {
        string? nextToken = null;

        var result = new List<T>();
        do
        {
            payload["page_token"] = nextToken;
            request.WithJsonBody(payload, JsonConfig.Settings);

            var response = await ExecuteWithErrorHandling<PaginationResponse<T>>(request, creds);

            nextToken = response.NextPageToken;
            result.AddRange(response.Results);
        } while (!string.IsNullOrWhiteSpace(nextToken));

        return result;
    }

    public async Task<List<T>> Paginate<T>(RestRequest request, AuthenticationCredentialsProvider[] creds)
    {
        string? nextToken = null;
        var baseUrl = request.Resource;

        var result = new List<T>();
        do
        {
            if (nextToken is not null)
                request.Resource = baseUrl.SetQueryParameter("page_token", nextToken);

            var response = await ExecuteWithErrorHandling<PaginationResponse<T>>(request, creds);

            nextToken = response.NextPageToken;
            result.AddRange(response.Results);
        } while (!string.IsNullOrWhiteSpace(nextToken));

        return result;
    }
}