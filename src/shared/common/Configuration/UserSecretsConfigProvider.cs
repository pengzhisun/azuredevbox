
namespace Winl.AzureDevBox.Configuration
{
    using Microsoft.Extensions.Configuration;

    public sealed class UserSecretsConfigProvider : IConfigProvider
    {
        private readonly IConfiguration config;

        public UserSecretsConfigProvider()
        {
            IConfigurationBuilder configBuilder =
                new ConfigurationBuilder().AddUserSecrets<UserSecretsConfigProvider>();

            this.config = configBuilder.Build();
        }

        public string Name => ConfigProviderNames.UserSecrets;

        public string Get(string configKey)
        {
            return this.config[configKey];
        }
    }
}