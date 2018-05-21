//------------------------------------------------------------------------------
// <copyright file="IDemo.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Winl.AzureDevBox.ConsoleDemo.Demos
{
    /// <summary>
    /// Defines the demo interface.
    /// </summary>
    public interface IDemo
    {
        #region Properties

        /// <summary>
        /// Gets the demo name.
        /// </summary>
        /// <value>
        /// The demo name.
        /// </value>
        string Name { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Runs the demo.
        /// </summary>
        void Run();

        #endregion
    }
}
