using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using Newtonsoft.Json.Linq;
using Amazon;

namespace Todolendar.API.Repositories
{
    public static class GetAWSSecret
    {
        static public async Task<string> GetSecret(string secretName, string value)
        {
            string region = "ap-southeast-2";

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT",
            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                throw e;
            }

            JObject json = JObject.Parse(response.SecretString);
            string connectionString = json.GetValue(value).ToString();

            return connectionString;
        }
    }
}
