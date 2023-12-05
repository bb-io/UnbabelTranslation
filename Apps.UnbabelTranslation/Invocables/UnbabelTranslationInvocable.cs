using Apps.UnbabelTranslation.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.UnbabelTranslation.Invocables;

public class UnbabelTranslationInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();
    
    protected UnbabelTranslationClient Client { get; }
    
    public UnbabelTranslationInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new();
    }
}