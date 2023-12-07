using Apps.UnbabelTranslation.Api;
using Apps.UnbabelTranslation.Constants;
using Apps.UnbabelTranslation.Enums;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.UnbabelTranslation.Invocables;

public class UnbabelTranslationInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected UnbabelTranslationClient TranslationClient { get; }
    protected UnbabelTranslationClient PipelineClient { get; }

    protected string CustomerId { get; }

    public UnbabelTranslationInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        TranslationClient = new();
        PipelineClient = new(ApiType.Pipelines);

        CustomerId = Creds.Get(CredsNames.CustomerId).Value;
    }
}