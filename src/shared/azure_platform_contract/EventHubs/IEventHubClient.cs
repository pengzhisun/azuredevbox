namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the event hub client interface.
    /// </summary>
    public interface IEventHubClient
    {
        #region Properties

        /// <summary>
        /// Gets the event hub connection string.
        /// </summary>
        /// <value>
        /// The event hub connection string.
        /// </value>
        string EventHubConnectionString { get; }

        /// <summary>
        /// Gets the entity path.
        /// </summary>
        /// <value>
        /// The entity path.
        /// </value>
        string EntityPath { get; }

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        string ClientId { get; }

        /// <summary>
        /// Gets the name of the event hub.
        /// </summary>
        /// <value>
        /// The name of the event hub.
        /// </value>
        string EventHubName { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the event hub runtime information asynchronous.
        /// </summary>
        /// <returns>The <see cref="IEventHubRuntimeInformation"/> instance.</returns>
        Task<IEventHubRuntimeInformation> GetEventHubRuntimeInformationAsync();

        /// <summary>
        /// Gets the partition runtime information asynchronous.
        /// </summary>
        /// <param name="partitionId">The partition identifier.</param>
        /// <returns>The <see cref="IEventHubPartitionRuntimeInformation"/> instance.</returns>
        Task<IEventHubPartitionRuntimeInformation> GetPartitionRuntimeInformationAsync(string partitionId);

        /// <summary>
        /// Sends string messages data in batch asynchronous.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>The asynchronous task for batch sending operation.</returns>
        Task BatchSendAsync(IEnumerable<string> messages);

        /// <summary>
        /// Sends bytes messages data in batch asynchronous.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>The asynchronous task for batch sending operation.</returns>
        Task BatchSendAsync(IEnumerable<byte[]> messages);

        /// <summary>
        /// Registers the event processor asynchronous.
        /// </summary>
        /// <param name="consumerGroupName">Name of the consumer group.</param>
        /// <param name="eventProcessor">The event processor.</param>
        /// <returns>The <see cref="IEventProcessorRegistration"/> instance.</returns>
        Task<IEventProcessorRegistration> RegisterAsync(IEventProcessor eventProcessor);

        #endregion
    }
}
