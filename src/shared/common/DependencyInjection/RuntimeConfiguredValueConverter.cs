namespace Winl.AzureDevBox.DependencyInjection
{
    using System.ComponentModel;
    using Winl.AzureDevBox.Configuration;

    public class RuntimeConfiguredValueConverter<TConfigProvider>
        : TypeConverter
        where TConfigProvider: IConfigProvider
    {
        
    }
}