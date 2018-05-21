namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    /// <summary>
    /// Defines the event hub error code enumeration.
    /// </summary>
    public enum EventHubErrorCode
    {
        /// <summary>
        /// The unknown error code.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The exceed batch size limitation error code.
        /// </summary>
        ExceedBatchSizeLimitation = 1001,
    }
}
