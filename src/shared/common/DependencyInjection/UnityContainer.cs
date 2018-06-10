namespace Winl.AzureDevBox.DependencyInjection
{
    using System;
    using System.Configuration;
    using System.IO;
    using Microsoft.Practices.Unity.Configuration;
    using Unity;
    using Unity.Injection;
    using Winl.AzureDevBox.Configuration;
    using InternalUnityContainer = Unity.UnityContainer;

    public sealed class UnityContainer
    {
        private static readonly string UnityConfigFileName =
            Path.Combine(AppContext.BaseDirectory, "azure_dev_box.unity.config");

        private static readonly InternalUnityContainer globalUnityContainer;

        private static readonly UnityContainer SingletonInstance =
            new UnityContainer();

        private readonly IUnityContainer internalUnityContainer;

        static UnityContainer()
        {
            InternalUnityContainer container = new InternalUnityContainer();
            container.RegisterSingleton<IConfigProvider>(
                ConfigProviderNames.UserSecrets,
                new InjectionFactory(c => new UserSecretsConfigProvider()));

            globalUnityContainer = container;
        }

        private UnityContainer()
        {
            IUnityContainer container =
                globalUnityContainer.CreateChildContainer();

            ExeConfigurationFileMap fileMap =
                new ExeConfigurationFileMap
                {
                    ExeConfigFilename = UnityConfigFileName
                };
            Configuration configuration =
                ConfigurationManager.OpenMappedExeConfiguration(
                    fileMap,
                    ConfigurationUserLevel.None);
            UnityConfigurationSection unitySection =
                (UnityConfigurationSection)configuration.GetSection("unity");

            container.LoadConfiguration(unitySection);

            this.internalUnityContainer = container;
        }

        public static UnityContainer Instance => SingletonInstance;

        public TInterface Resolve<TInterface>()
        {
            return this.internalUnityContainer.Resolve<TInterface>();
        }
    }
}