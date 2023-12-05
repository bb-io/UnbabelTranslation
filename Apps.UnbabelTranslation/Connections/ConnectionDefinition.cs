using Apps.UnbabelTranslation.Constants;
using Apps.UnbabelTranslation.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.UnbabelTranslation.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "Basic",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.UserName) { DisplayName = "Username" },
                new(CredsNames.Password) { DisplayName = "Password", Sensitive = true }
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var token = GetAccessToken(values);

        yield return new(AuthenticationCredentialsRequestLocation.Body, CredsNames.AccessToken, token);
    }

    private async Task<CredentialsResponse> GetAccessToken(Dictionary<string, string> values)
    {
        var client = new RestClient();

        var formPayload = new Dictionary<string, string>()
        {
            ["grant_type"] = "password",
            ["username"] = values[CredsNames.UserName],
            ["password"] = values[CredsNames.Password],
            ["client_id"] = "translation-api-os",
        };
        var request = new RestRequest(Urls.Token, Method.Post)
            .WithFormData(formPayload);

        var response = await client.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<CredentialsResponse>(response.Content!, JsonConfig.Settings)!;
    }
}