namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the event hub exception class.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class EventHubException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubException"/> class.
        /// </summary>
        public EventHubException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EventHubException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public EventHubException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected EventHubException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.ErrorCode = (EventHubErrorCode)info.GetValue(nameof(this.ErrorCode), typeof(EventHubErrorCode));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public EventHubErrorCode ErrorCode { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(this.ErrorCode), this.ErrorCode);
        }

        #endregion
    }
}
