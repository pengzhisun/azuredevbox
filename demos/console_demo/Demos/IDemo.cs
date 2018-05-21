//------------------------------------------------------------------------------
// <copyright file="IDemo.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

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
