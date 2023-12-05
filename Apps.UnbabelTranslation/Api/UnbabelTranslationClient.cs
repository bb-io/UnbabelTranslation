using Apps.UnbabelTranslation.Constants;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.UnbabelTranslation.Api;

public class UnbabelTranslationClient : BlackBirdRestClient
{
    public UnbabelTranslationClient() : base(new()
    {
        BaseUrl = Urls.Api.ToUri()
    })
    {
    }

    public override Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        return base.ExecuteWithErrorHandling(request);
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        throw new NotImplementedException();
    }
}