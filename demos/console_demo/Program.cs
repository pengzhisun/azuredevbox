//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.ConsoleDemo
{
    using System;
    using System.Diagnostics;
    using Winl.AzureDevBox.ConsoleDemo.Demos;

    /// <summary>
    /// Defines the console demo application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            // RunDemo<ChecksDemo>();
            RunDemo<EventHubDemo>();
        }

        /// <summary>
        /// Runs the demo.
        /// </summary>
        /// <typeparam name="TDemo">The type of the demo.</typeparam>
        private static void RunDemo<TDemo>()
            where TDemo : class, IDemo
        {
            TDemo demo = Activator.CreateInstance<TDemo>();
            demo.Run();
        }
    }
}
