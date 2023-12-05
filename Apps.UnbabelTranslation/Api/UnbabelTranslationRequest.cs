using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.UnbabelTranslation.Api;

public class UnbabelTranslationRequest : BlackBirdRestRequest
{
    public UnbabelTranslationRequest(string resource, Method method,
        IEnumerable<AuthenticationCredentialsProvider> creds) : base(resource, method, creds)
    {
    }

    protected override void AddAuth(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        throw new NotImplementedException();
    }
}