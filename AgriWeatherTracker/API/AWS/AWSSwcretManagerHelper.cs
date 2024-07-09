using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System;
using System.Threading.Tasks;

public static class AWSSecretsManagerHelper
{
    public static async Task<string> GetSecretValueAsync(string secretName, RegionEndpoint region)
    {
        using (var client = new AmazonSecretsManagerClient(region))
        {
            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            var response = await client.GetSecretValueAsync(request);

            if (response.SecretString != null)
            {
                return response.SecretString;
            }

            throw new Exception("Secret value is null");
        }
    }
}
