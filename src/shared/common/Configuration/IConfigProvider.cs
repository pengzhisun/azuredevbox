namespace Winl.AzureDevBox.Configuration
{
    public interface IConfigProvider
    {
        string Name { get; }

        string Get(string configKey);
    }
}